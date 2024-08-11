using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;

public class GestureFingerBend : MonoBehaviour
{
    public GameObject targetButton;
    public float bendThreshold = 0.01f;

    void Update()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose indexTipPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexKnuckle, Handedness.Right, out MixedRealityPose indexKnucklePose))
        {
            float distance = Vector3.Distance(indexTipPose.Position, indexKnucklePose.Position);

            if (distance < bendThreshold)
            {
                TriggerButton();
            }
        }
    }

    void TriggerButton()
    {
        var interactable = targetButton.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.TriggerOnClick();
        }
        else
        {
            Debug.LogWarning("No Interactable component found on the target button.");
        }
    }
}
