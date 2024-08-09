using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using System.Collections;

public class GestureWishAway2 : MonoBehaviour
{
    public ViewController _viewController;
    public InfoPaintingMenuView _infoPaintingMenuView;
    private float gestureDistance = 0.1f;
    private float gestureTime = 0.5f;
    public float delayDuration = 5.0f;
    private bool canDetectSwipe;

    private Vector3 startPositionL;
    private Vector3 startPositionR;
    private float gestureStartTime;
    private bool gestureInProgress = false;

    void Start(){
        _viewController = FindObjectOfType<ViewController>();
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
        if(_viewController.IntroductionState) {
            if (_viewController.WelcomeScreen.activeSelf)
            {
                _viewController.IntroductionScreen.SetActive(true);
                _viewController.WelcomeScreen.SetActive(false);
            }
            else if (_viewController.IntroductionScreen.activeSelf)
            {
                _viewController.IntroductionScreen.SetActive(false);

                // start image recognition
                _viewController.StartImageRecognition();
            }
        } else if (_infoPaintingMenuView.InfoPanelState) {
            // TODO panels
        }
        
    }

    private IEnumerator TouchDelay() {
        canDetectSwipe = false;
        yield return new WaitForSeconds(delayDuration);
        canDetectSwipe = true;
    }

}
