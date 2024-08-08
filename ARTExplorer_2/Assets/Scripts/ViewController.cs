using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField]
    public GameObject welcomeScreen {get;}
     [SerializeField]
    public GameObject introductionScreen{get;}
     [SerializeField]
    public GameObject imageTargetHarbor{get;}
     [SerializeField]
    public GameObject imageTargetDinner{get;}
    
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
