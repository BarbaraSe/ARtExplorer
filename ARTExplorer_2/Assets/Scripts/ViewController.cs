using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomeScreen;
    [SerializeField]
    private GameObject introductionScreen;
    [SerializeField]
    private GameObject imageTargetHarbor;
    [SerializeField]
    private GameObject imageTargetDinner;
    public GameObject WelcomeScreen {set; get;}
    public GameObject IntroductionScreen{set; get;}
    public GameObject ImageTargetHarbor{set; get;}
    public GameObject ImageTargetDinner{set; get;}

    
    void Start()
    {    
        WelcomeScreen = welcomeScreen;
        IntroductionScreen = introductionScreen;
        ImageTargetHarbor = imageTargetHarbor;
        ImageTargetDinner = imageTargetDinner;

        welcomeScreen.SetActive(true);
        introductionScreen.SetActive(false);
        imageTargetDinner.SetActive(false);
        imageTargetHarbor.SetActive(false);
    }

    void Update()
    {
        
    }
}
