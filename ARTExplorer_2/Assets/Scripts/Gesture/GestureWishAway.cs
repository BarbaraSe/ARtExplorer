using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using UnityEngine;

public class GestureWishAway : MonoBehaviour
{
    public GameObject gameObject; 
    private ViewController viewController;
    private UIMenuController uIButtonController;
    private float gestureDistance = 0.1f;
    private float gestureTime = 0.5f;

    private Vector3 startPositionL;
    private Vector3 startPositionR;
    private float gestureStartTime;
    private bool gestureInProgress = false;
    public float delayDuration = 5.0f;  // Delay duration in seconds

    private bool canDetectTouch = true;

    void Start(){
        viewController = FindObjectOfType<ViewController>();
        uIButtonController = FindObjectOfType<UIMenuController>();
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

        /*if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out MixedRealityPose palmPoseL2) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out MixedRealityPose palmPoseR2)) {
            
            Vector3 currentPositionL = palmPoseL2.Position;
            Vector3 currentPositionR = palmPoseR2.Position;

            if (!gestureInProgress)
            {
                startPositionL = currentPositionL;
                startPositionR = currentPositionR;
                gestureStartTime = Time.time;
                gestureInProgress = true;
            }
            else
            {
                if (Vector3.Distance(startPositionL, currentPositionL) >= gestureDistance &&
                    Vector3.Distance(startPositionR, currentPositionR) >= gestureDistance)
                {
                    if (Time.time - gestureStartTime <= gestureTime)
                    {
                        //CloseCube();
                        Debug.Log("Gesture detected, undoing action");
                        uIButtonController.UndoLastAction();
                    }
                    gestureInProgress = false;
                }
                else if (Time.time - gestureStartTime > gestureTime)
                {
                    gestureInProgress = false;
                }
            }
            }*/
    }

    void OpenCloseMenu()
    {
        if (viewController.WelcomeScreen != null && viewController.WelcomeScreen.activeSelf)
        {
            viewController.IntroductionScreen.SetActive(true);
            viewController.WelcomeScreen.SetActive(false);
        }
        else if (viewController.IntroductionScreen != null && viewController.IntroductionScreen.activeSelf)
        {
            viewController.IntroductionScreen.SetActive(false);

            // start image recognition
            if (viewController.ImageTargetHarbor != null && viewController.ImageTargetDinner != null ) {
                viewController.ImageTargetHarbor.SetActive(true);
                viewController.ImageTargetDinner.SetActive(true);
            } else {
                Debug.LogWarning("ImageTargetHarbor and/or ImageTargetDinner are empty");
            }
           
        }
    }

    /*void CloseCube(){
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
    }*/

    private IEnumerator TouchDelay()
    {
        // Prevent further touch detection
        canDetectTouch = false;

        // Wait for the specified delay duration
        yield return new WaitForSeconds(delayDuration);

        // Allow touch detection again
        canDetectTouch = true;
    }
}
