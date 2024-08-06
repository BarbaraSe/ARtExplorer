using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class CustomHandGestureMenuOpener : MonoBehaviour
{
    public GameObject menu; // The menu to open
    public float activationDistance = 0.04f; // The threshold distance for a pinch gesture

    private bool isMenuOpen = false;

    void Update()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose indexTipPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out MixedRealityPose thumbTipPose))
        {
            float distance = Vector3.Distance(indexTipPose.Position, thumbTipPose.Position);

            // If the hand is pinching and the menu is not open, open it
            if (distance < activationDistance && !isMenuOpen)
            {
                CloseMenu();
            }
            // Optionally, add code to close the menu if the gesture is detected again
        }
    }

    void OpenMenu()
    {
        menu.SetActive(true);
        isMenuOpen = true;
    }

    void CloseMenu()
    {
        menu.SetActive(false);
        isMenuOpen = false;
    }
}
