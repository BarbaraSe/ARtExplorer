using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class GesturePinch : MonoBehaviour
{
    public GameObject menu; 
    public float activationDistance = 0.02f; 

    private bool isMenuOpen = false;

    void Update()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out MixedRealityPose indexTipPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out MixedRealityPose thumbTipPose))
        {
            float distance = Vector3.Distance(indexTipPose.Position, thumbTipPose.Position);

            if (distance < activationDistance && !isMenuOpen)
            {
                OpenCloseMenu();
            }
        }
    }

    void OpenCloseMenu()
    {
        if (!isMenuOpen) {
            menu.SetActive(true);
            isMenuOpen = true;
        } else {
            menu.SetActive(false);
            isMenuOpen = false;
        }
    }
}
