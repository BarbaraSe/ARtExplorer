using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using System.Collections;

public class GestureUndo : MonoBehaviour
{
    public GameObject gameObject; 
    private Vector3 pinkyTipRStart;
    private Vector3 pinkyKnuckleRStart;
    private Vector3 palmRStart;
    private Vector3 pinkyTipLStart;
    private Vector3 pinkyKnuckleLStart;
    private Vector3 palmLStart;
    private bool initialMovementDone = false;
    private bool handsMovedApart = false;
    private bool fingerTouching = false;

    public float separationDistance = 0.2f;
    public float closingDistance = 0.01f;
    public float gestureTime = 0.05f;   

    private float gestureStartTime;
    private int countGesture;

    void Update()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out MixedRealityPose pinkyTipR) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyKnuckle, Handedness.Right, out MixedRealityPose pinkyKnuckleR) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Left, out MixedRealityPose pinkyTipL) && 
            HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyKnuckle, Handedness.Left, out MixedRealityPose pinkyKnuckleL))
        {

            Debug.Log($"Start Left {pinkyTipL.Position} Start Right {pinkyTipR.Position}");

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
                float initDistancePinkyTip = Vector3.Distance(pinkyTipRStart, pinkyTipLStart);
                float initDistancePinkyKnuckle = Vector3.Distance(pinkyKnuckleRStart, pinkyKnuckleLStart);
                float currentDistancePinkyTip = Vector3.Distance(pinkyTipR.Position, pinkyTipL.Position);
                float currentDistancePinkyKnuckle = Vector3.Distance(pinkyKnuckleR.Position, pinkyKnuckleL.Position);

                Debug.Log($"Init Distance {initDistancePinkyTip} currentDistancePinkyTip {pinkyTipR.Position}");
                if (!fingerTouching && Mathf.Abs(currentDistancePinkyTip - initDistancePinkyTip) < closingDistance &&
                    Mathf.Abs(currentDistancePinkyKnuckle - initDistancePinkyKnuckle) < closingDistance){
                    fingerTouching = true;
                }

                if (fingerTouching && !handsMovedApart && currentDistancePinkyTip > separationDistance &&
                    currentDistancePinkyKnuckle > separationDistance)
                {
                    handsMovedApart = true;
                    Debug.LogWarning("Hands moved apart");
                
                    if (Time.time - gestureStartTime <= gestureTime)
                    {
                        CompleteGesture();
                    }
                } 

                if (fingerTouching && !handsMovedApart && Time.time - gestureStartTime > gestureTime)
                {
                    ResetGesture();
                }
            }
        }    

    }

    void CompleteGesture()
    {
        if (gameObject != null)
        {
            if (!gameObject.activeSelf) {
                gameObject.SetActive(true);
            } else {
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("gameObject not assigned.");
        }
        countGesture += 1;
        Debug.Log($"Count gesture: {countGesture} ");
        StartCoroutine(WaitAndExecute());
        ResetGesture();
    }

    void ResetGesture()
    {
        Debug.LogWarning("RESET GESTURE");
        initialMovementDone = false;
        handsMovedApart = false;
        countGesture = 0;
    }

    private IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(2f);  
    }
}
