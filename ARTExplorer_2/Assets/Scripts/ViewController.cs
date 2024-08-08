using System.Collections;
using UnityEngine;
using TMPro;

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
    [SerializeField]
    private TMP_Text swipeText;
    private float delay = 2f;
    
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

        swipeText.gameObject.SetActive(false);
        welcomeScreen.SetActive(true);
        introductionScreen.SetActive(false);
        imageTargetDinner.SetActive(false);
        imageTargetHarbor.SetActive(false);

        StartCoroutine(EnableTextCoroutine());
    }

    private IEnumerator EnableTextCoroutine()
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        swipeText.gameObject.SetActive(true); // Enable the text
    }
}
