using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomeScreen {get;};
     [SerializeField]
    private GameObject introductionScreen{get;};
     [SerializeField]
    private GameObject imageTargetHarbor{get;};
     [SerializeField]
    private GameObject imageTargetDinner{get;};
    
    void Start()
    {
        welcomeScreen.SetActive(true);
        introductionScreen.SetActive(false);
        imageTargetDinner.SetActive(false);
        imageTargetHarbor.SetActive(false);
    }

    void Update()
    {
        
    }
}
