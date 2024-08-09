using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using UnityEngine;

public class GestureWishAway : MonoBehaviour
{
    private ViewController _viewController;
    private UIMenuController _uIButtonController;
    private PaintingInfoAboutPaintingView _infoPaintingMenuView;
    private IntroScreensView _introScreensView;
    private float gestureDistance = 0.1f;
    private float gestureTime = 0.5f;

    private Vector3 startPositionL;
    private Vector3 startPositionR;
    private float gestureStartTime;
    private bool gestureInProgress = false;
    public float delayDuration = 5.0f;  // Delay duration in seconds

    private bool canDetectSwipe = true;

    void Start(){
        _viewController = FindObjectOfType<ViewController>();
        _uIButtonController = FindObjectOfType<UIMenuController>();
        _introScreensView = FindObjectOfType<IntroScreensView>();

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
                if (Vector3.Distance(startPositionR, currentPositionR) >= gestureDistance &&  canDetectSwipe)
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
        if(_introScreensView.GetIntroductionScreens().activeSelf) {
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
                _introScreensView.SetIntroductionScreens(false);
                if(_introScreensView.welcome) {
                    _viewController.StartImageRecognition();
                }
            }
        } else if (_infoPaintingMenuView.GetPanelActiveState()) {
            // TODO panels
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
