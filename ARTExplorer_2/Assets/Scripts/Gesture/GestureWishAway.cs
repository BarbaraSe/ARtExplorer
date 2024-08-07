using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class GestureWishAway : MonoBehaviour
{
    public GameObject welcomeMenu; 
    public GameObject instructionMenu;
    public GameObject sphere;
    public float gestureDistance = 0.1f;  
    public float gestureTime = 0.5f;
    
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
        if (welcomeMenu.activeSelf) {
            instructionMenu.SetActive(true);
            welcomeMenu.SetActive(false);
        } else if (instructionMenu.activeSelf) {
            instructionMenu.SetActive(false);
            sphere.SetActive(true);
            
        }
    }
}
