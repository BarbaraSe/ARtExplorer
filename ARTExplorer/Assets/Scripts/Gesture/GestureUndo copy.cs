using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class GestureUndoOld : MonoBehaviour
{
    private bool isMenuOpen = true;
    private Vector3 indexTipRStart;
    private Vector3 indexTipLStart;
    private bool initialMovementDone = false;
    private bool handsCrossed = false;
    private bool occlusionDetected = false;
    private bool handsMovedApart = false;
    private bool fingerTouching = false;

    // Thresholds
    public float crossingThreshold = 0.03f;
    public float occlusionThreshold = 0.02f;
    public float separationDistance = 0.15f;
    public float gestureTime = 5.0f;

    private float gestureStartTime;

    void Update()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out MixedRealityPose indexTipL) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose indexTipR))
        {
            Debug.Log($"Right Hand Position: {indexTipL.Position} AND Left Hand Position: {indexTipR.Position}");
            if (!initialMovementDone)
            {
                indexTipRStart = indexTipL.Position;
                indexTipLStart = indexTipR.Position;
                initialMovementDone = true;
            }
            else
            {
                // Calculate distances and update gesture state
                float initDistanceIndexTip = Vector3.Distance(indexTipRStart, indexTipLStart);
                float currentDistanceIndexTip = Vector3.Distance(indexTipR.Position, indexTipL.Position);

                Debug.Log($"init distance: {initDistanceIndexTip} AND current distance: {currentDistanceIndexTip}");

                // Detect crossing of hands
                if (!handsCrossed && Mathf.Abs(currentDistanceIndexTip - initDistanceIndexTip) > crossingThreshold)
                {
                    handsCrossed = true;
                    Debug.LogWarning("Hands Crossed");
                }

                // Detect occlusion
                if (handsCrossed && !occlusionDetected && currentDistanceIndexTip < occlusionThreshold)
                {
                    occlusionDetected = true;
                    Debug.LogWarning("Occlusion Detected");
                }

                // Detect hands moving apart
                if (occlusionDetected && !handsMovedApart && currentDistanceIndexTip > separationDistance)
                {
                    handsMovedApart = true;
                    Debug.LogWarning("Hands moved apart");
                }

                // Check if the gesture is completed within the allowed time
                if (handsMovedApart && Time.time - gestureStartTime <= gestureTime)
                {
                    CompleteGesture();
                    Debug.LogWarning("Within Allowed Time");
                }

                // Reset if the gesture is incomplete or the time exceeds
                if (Time.time - gestureStartTime > gestureTime || !handsMovedApart)
                {
                    ResetGesture();
                }
            }
        }
        else
        {
            ResetGesture();
        }
    }

    void CompleteGesture()
    {
        if (gameObject != null)
        {
            if (!isMenuOpen)
            {
                gameObject.SetActive(true);
                isMenuOpen = true;
            }
            else
            {
                gameObject.SetActive(false);
                isMenuOpen = false;
            }
            Debug.Log("Complex gesture detected! Activating gameObject.");
        }
        else
        {
            Debug.LogWarning("gameObject not assigned.");
        }
    }

    void ResetGesture()
    {
        initialMovementDone = false;
        //handsCrossed = false;
        //occlusionDetected = false;
        handsMovedApart = false;
    }
}
