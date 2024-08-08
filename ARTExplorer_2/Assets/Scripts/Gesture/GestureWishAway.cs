using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class GestureWishAway : MonoBehaviour
{
    ViewController viewController;
    private float gestureDistance = 0.1f;  
    private float gestureTime = 0.5f;
    
    private Vector3 startPosition;
    private float gestureStartTime;
    private bool gestureInProgress = false;

    void Update()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out MixedRealityPose palmPose))
        {
            Vector3 currentPosition = palmPose.Position;

            if (!gestureInProgress)
            {
                startPosition = currentPosition;
                gestureStartTime = Time.time;
                gestureInProgress = true;
            }
            else
            {
                if (Vector3.Distance(startPosition, currentPosition) >= gestureDistance)
                {
                    if (Time.time - gestureStartTime <= gestureTime)
                    {
                        OpenCloseMenu();
                    }
                    gestureInProgress = false;
                }
                else if (Time.time - gestureStartTime > gestureTime)
                {
                    gestureInProgress = false;
                }
            }
        }
        else
        {
            gestureInProgress = false;
        }
    }

    void OpenCloseMenu()
    {
        if (viewController.WelcomeScreen.activeSelf) {
            viewController.IntroductionScreen.SetActive(true);
            viewController.WelcomeScreen.SetActive(false);
        } else if (viewController.IntroductionScreen.activeSelf) {
            viewController.IntroductionScreen.SetActive(false);

            // start image recognition
            viewController.ImageTargetHarbor.SetActive(true);
            viewController.ImageTargetDinner.SetActive(true);
        }
    }
}
