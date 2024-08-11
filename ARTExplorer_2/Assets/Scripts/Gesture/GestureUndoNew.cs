using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using Microsoft.MixedReality.Toolkit.Utilities;

public class GestureUndoNew : MonoBehaviour
{
    public UIMenuController gestureController;
    public float pinkyTouchThreshold = 0.02f;  // Distance threshold to consider the pinky touch
    public float clapThreshold = 0.05f;  // Distance threshold to consider a clap
    public float delayDuration = 5.0f;  // Delay duration in seconds

    private bool canDetectPinkyTouch = true;
    private bool canDetectClap = true;


    void Update()
    {
        // Detect pinky
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Left, out MixedRealityPose leftPinkyPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out MixedRealityPose rightPinkyPose))
        {

            float pinkyDistance = Vector3.Distance(leftPinkyPose.Position, rightPinkyPose.Position);
            if (pinkyDistance < pinkyTouchThreshold && canDetectPinkyTouch)
            {
                gestureController.UndoLastAction();
                StartCoroutine(PinkyTouchDelay());
            }
        }

        // Detect clap
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out MixedRealityPose leftPalmPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out MixedRealityPose rightPalmPose))
        {
            float clapDistance = Vector3.Distance(leftPalmPose.Position, rightPalmPose.Position);
            if (clapDistance < clapThreshold && canDetectClap)
            {
                Debug.Log("clap");
                gestureController.UndoEverythingButton();
                StartCoroutine(ClapDelay());
            }
        }
    }

    // Prevent multiple pinky touch detection
    private IEnumerator PinkyTouchDelay()
    {
        canDetectPinkyTouch = false;
        yield return new WaitForSeconds(5);
        Debug.Log("Waiting");
        canDetectPinkyTouch = true;
    }

    private IEnumerator ClapDelay()
    {
        canDetectClap = false;
        yield return new WaitForSeconds(5);
        Debug.Log("Waiting");
        canDetectClap = true;
    }
}
