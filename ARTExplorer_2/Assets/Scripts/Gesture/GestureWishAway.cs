using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class GestureWishAway : MonoBehaviour
{
    public ViewController viewController;
    private float gestureDistance = 0.1f;
    private float gestureTime = 0.5f;

    private Vector3 startPositionL;
    private Vector3 startPositionR;
    private float gestureStartTime;
    private bool gestureInProgress = false;

    void Start(){
        viewController = FindObjectOfType<ViewController>();
    }

    void Update()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out MixedRealityPose palmPoseL))
        {
            Vector3 currentPositionL = palmPoseL.Position;

            if (!gestureInProgress)
            {
                startPositionL = currentPositionL;
                gestureStartTime = Time.time;
                gestureInProgress = true;
            }
            else
            {
                if (Vector3.Distance(startPositionL, currentPositionL) >= gestureDistance)
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
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out MixedRealityPose palmPoseRight))
        {
            Vector3 currentPositionR = palmPoseRight.Position;

            if (!gestureInProgress)
            {
                startPositionR = currentPositionR;
                gestureStartTime = Time.time;
                gestureInProgress = true;
            }
            else
            {
                if (Vector3.Distance(startPositionR, currentPositionR) >= gestureDistance)
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
        if (viewController.WelcomeScreen.activeSelf)
        {
            viewController.IntroductionScreen.SetActive(true);
            viewController.WelcomeScreen.SetActive(false);
        }
        else if (viewController.IntroductionScreen.activeSelf)
        {
            viewController.IntroductionScreen.SetActive(false);

            // start image recognition
           viewController.StartImageRecognition();
        }
    }
}
