using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using UnityEngine;

public class GestureWishAway : MonoBehaviour
{
    private ViewController _viewController;
    private PaintingInfoScreensController _paintingInfoScreensController;
    private IntroScreensView _introScreensView;
    private float gestureDistance = 0.1f;
    private float gestureTime = 0.5f;

    private Vector3 startPositionL;
    private Vector3 startPositionR;
    private float gestureStartTime;
    private bool gestureInProgress = false;
    public float delayDuration = 2.0f;  // Delay duration in seconds

    private bool canDetectSwipe = true;

    void Start()
    {
        _viewController = FindObjectOfType<ViewController>();
        _introScreensView = FindObjectOfType<IntroScreensView>();
        _paintingInfoScreensController = FindObjectOfType<PaintingInfoScreensController>();
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
                if (Vector3.Distance(startPositionL, currentPositionL) >= gestureDistance && canDetectSwipe)
                {
                    if (Time.time - gestureStartTime <= gestureTime)
                    {
                        Debug.Log("Gesture detected, handling menu");
                        OpenCloseMenu();
                        StartCoroutine(TouchDelay());
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
                if (Vector3.Distance(startPositionR, currentPositionR) >= gestureDistance && canDetectSwipe)
                {
                    if (Time.time - gestureStartTime <= gestureTime)
                    {
                        Debug.Log("Gesture detected, handling menu");
                        OpenCloseMenu();
                        StartCoroutine(TouchDelay());
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
        if (_viewController.GetIntroductionScreen().activeSelf)
        {
            if (_introScreensView.GetWelcomePanel().activeSelf)
            {
                _introScreensView.SetWelcomePanelActive(false);
                _introScreensView.SetIntroductionPanel1Active(true);
            }
            else if (_introScreensView.GetIntroductionPanel1().activeSelf)
            {
                _introScreensView.SetIntroductionPanel1Active(false);
                _introScreensView.SetIntroductionPanel2Active(true);
            }
            else if (_introScreensView.GetIntroductionPanel2().activeSelf)
            {
                _introScreensView.SetIntroductionPanel2Active(false);
                _viewController.SetIntroductionScreensActive(false);
                if (_introScreensView.welcome)
                {
                    _viewController.StartImageRecognition();
                }
                _introScreensView.welcome = false;
            }
        }
        else if (_viewController.GetInfoScreensCanvas().activeSelf)
        {
            if (_paintingInfoScreensController.GetStartScreenInfoPanel().activeSelf)
            {
                _viewController.SetInfoScreensActive(false);
            }
            else if (_paintingInfoScreensController.GetAboutInfo().activeSelf)
            {
                _paintingInfoScreensController.SetAboutInfoActive(false);
                _paintingInfoScreensController.SetStartScreenInfoActive(true);
            }
            else if (_paintingInfoScreensController.GetPaintingInfo().activeSelf)
            {
                _paintingInfoScreensController.SetPaintingInfoActive(false);
                _paintingInfoScreensController.SetStartScreenInfoActive(true);
            }
        }
    }
    private IEnumerator TouchDelay()
    {
        canDetectSwipe = false;
        yield return new WaitForSeconds(delayDuration);
        Debug.Log("Waiting");
        canDetectSwipe = true;
    }
}
