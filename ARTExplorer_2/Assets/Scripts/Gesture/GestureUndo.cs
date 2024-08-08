using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class GestureUndo : MonoBehaviour
{
    public GameObject gameObject; 
    private bool isMenuOpen = true;
    
    private Vector3 pinkyTipRStart;
    private Vector3 pinkyKnuckleRStart;
    private Vector3 palmRStart;
    private Vector3 pinkyTipLStart;
    private Vector3 pinkyKnuckleLStart;
    private Vector3 palmLStart;
    private bool initialMovementDone = false;
    //private bool handsCrossed = false;
    //private bool occlusionDetected = false;
    private bool handsMovedApart = false;
    private bool fingerTouching = false;

    // Thresholds
    //public float crossingThreshold = 0.03f;  
    //public float occlusionThreshold = 0.02f;
    public float separationDistance = 0.2f;
    public float closingDistance = 0.01f;
    public float gestureTime = 3.0f;   

    private float gestureStartTime;

    void Update()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out MixedRealityPose pinkyTipR) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyKnuckle, Handedness.Right, out MixedRealityPose pinkyKnuckleR) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Left, out MixedRealityPose pinkyTipL) && 
            HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyKnuckle, Handedness.Left, out MixedRealityPose pinkyKnuckleL))
        {
            if (!initialMovementDone)
            {
                pinkyTipRStart = pinkyTipR.Position;
                pinkyKnuckleRStart = pinkyKnuckleR.Position;

                pinkyTipLStart = pinkyTipL.Position;
                pinkyKnuckleLStart = pinkyKnuckleL.Position;
                gestureStartTime = Time.time;
                initialMovementDone = true;
            }
            else
            {
                // Calculate distances and update gesture state
                float initDistancePinkyTip = Vector3.Distance(pinkyTipRStart, pinkyTipLStart);
                float initDistancePinkyKnuckle = Vector3.Distance(pinkyKnuckleRStart, pinkyKnuckleLStart);

                float currentDistancePinkyTip = Vector3.Distance(pinkyTipR.Position, pinkyTipL.Position);
                float currentDistancePinkyKnuckle = Vector3.Distance(pinkyKnuckleR.Position, pinkyKnuckleL.Position);

                if (!fingerTouching && (Mathf.Abs(currentDistancePinkyTip - initDistancePinkyTip) < closingDistance &&
                    Mathf.Abs(currentDistancePinkyKnuckle - initDistancePinkyKnuckle) < closingDistance)){
                    fingerTouching = true;
                }

                if (fingerTouching && !handsMovedApart && (currentDistancePinkyTip > separationDistance &&
                    currentDistancePinkyKnuckle > separationDistance))
                {
                    handsMovedApart = true;
                    Debug.LogWarning("Hands moved apart");
                }

                //if (fingerTouching && handsMovedApart) {
                //    CompleteGesture();
                //}

                // Check if the gesture is completed within the allowed time
                if (handsMovedApart&& handsMovedApart && Time.time - gestureStartTime <= gestureTime)
                {
                    CompleteGesture();
                    ResetGesture();
                    Debug.LogWarning("Within Allowed Time");
                }

                // Reset if the gesture is incomplete or the time exceeds
                if (Time.time - gestureStartTime > gestureTime || !handsMovedApart)
                {
                    ResetGesture();
                   // Debug.LogWarning("Reset Gesture Time");
                }
                //Debug.Log($"Current Distance: {currentDistance}");
            }
            
        
        }
        else
        {
            ResetGesture();
            //Debug.LogWarning("Reset Gesture Didnt detect anything");
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
        handsMovedApart = false;
    }
}
