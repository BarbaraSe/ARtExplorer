using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class GestureUndo : MonoBehaviour
{
    public GameObject gameObject; 
    private bool isMenuOpen = true;
    
    private Vector3 rightHandStart;
    private Vector3 leftHandStart;
    private bool initialMovementDone = false;
    private bool handsCrossed = false;
    private bool occlusionDetected = false;
    private bool handsMovedApart = false;

    // Thresholds
    public float crossingThreshold = 0.03f;  // Minimum crossing distance (in meters)
    public float occlusionThreshold = 0.02f; // Threshold for detecting occlusion (in meters)
    public float separationDistance = 0.05f; // Minimum distance after separation (in meters)
    public float gestureTime = 5.0f;         // Maximum time allowed to complete the gesture (in seconds)

    private float gestureStartTime;

    void Update()
    {

        

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out MixedRealityPose rightHandPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out MixedRealityPose leftHandPose))
        {
            Debug.LogWarning("PALM DETECTED");
            //Debug.Log($"Right Hand Position: {rightHandPose.Position}");
            //Debug.Log($"Left Hand Position: {leftHandPose.Position}");
            if (!initialMovementDone)
            {
                rightHandStart = rightHandPose.Position;
                leftHandStart = leftHandPose.Position;
                gestureStartTime = Time.time;
                initialMovementDone = true;
            }
            else
            {
                // Calculate distances and update gesture state
                float initialDistance = Vector3.Distance(rightHandStart, leftHandStart);
                float currentDistance = Vector3.Distance(rightHandPose.Position, leftHandPose.Position);

                // Detect crossing of hands
                if (!handsCrossed && Mathf.Abs(currentDistance - initialDistance) > crossingThreshold)
                {
                    handsCrossed = true;
                    Debug.LogWarning("Hands Crossed");
                }

                // Detect occlusion
                if (handsCrossed && !occlusionDetected && currentDistance < occlusionThreshold)
                {
                    occlusionDetected = true;
                    Debug.LogWarning("Occlusion Detected");
                }

                // Detect hands moving apart
                if (occlusionDetected && !handsMovedApart && currentDistance > separationDistance)
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
                    Debug.LogWarning("Reset Gesture");
                }
                //Debug.Log($"Current Distance: {currentDistance}");
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
            if (!isMenuOpen) {
                gameObject.SetActive(true);
                isMenuOpen = true;
            } else {
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
        handsCrossed = false;
        occlusionDetected = false;
        handsMovedApart = false;
    }
}
