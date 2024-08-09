using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;
using Unity.XR.CoreUtils;

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
    [SerializeField]
    private GameObject infoButton;
    private float delay = 2f;
    private GameObject[] paintings;

    public GameObject WelcomeScreen {set; get;}
    public GameObject IntroductionScreen{set; get;}

    public bool IntroductionState {set; get;}

    void Start()
    {    
        IntroductionState = true;
        WelcomeScreen = welcomeScreen;
        IntroductionScreen = introductionScreen;
        paintings = new GameObject[2]{imageTargetHarbor, imageTargetDinner};

        //welcomeScreen.SetActive(true);
        //swipeText.gameObject.SetActive(false);
        //introductionScreen.SetActive(false);
        //imageTargetDinner.SetActive(false);
        //imageTargetHarbor.SetActive(false);
        //StartCoroutine(EnableTextCoroutine());

        StartImageRecognition(); // löschen !!!
    }

    public void StartImageRecognition(){
        foreach(var painting in paintings) {
            painting.SetActive(true);
            AddInfoButtonToPainting(painting);
        }
        IntroductionState = false;
    }

    private void AddInfoButtonToPainting(GameObject painting){
        GameObject childCopyBtn = Instantiate(infoButton);
        GameObject infoButtonMenu = childCopyBtn.transform.Find("InfoMenuDetail").gameObject;

        childCopyBtn.transform.SetParent(painting.transform);
        childCopyBtn.SetActive(true);
        infoButtonMenu.SetActive(false);
        childCopyBtn.AddComponent<InfoMenu>();
        childCopyBtn.transform.name = "InfoBtn";
        childCopyBtn.transform.localPosition = new Vector3(0.5f, 0, 0);
        //infoButtonMenu.transform.localPosition = new Vector3(0.5f, 0, 0);
    }

    private IEnumerator EnableTextCoroutine()
    {
        yield return new WaitForSeconds(delay);
        swipeText.gameObject.SetActive(true);
    }
}
