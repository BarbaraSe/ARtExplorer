using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    private GameObject infoButton;
     [SerializeField]
    private GameObject infoButtonMenu;

    private float delay = 2f;
    private GameObject[] paintings;

    public GameObject WelcomeScreen {set; get;}
    public GameObject IntroductionScreen{set; get;}

    private bool introductionState;

    void Start()
    {    
        introductionState = true;
        WelcomeScreen = welcomeScreen;
        IntroductionScreen = introductionScreen;
        paintings = new GameObject[2]{imageTargetHarbor, imageTargetDinner};

        //swipeText.gameObject.SetActive(false);
        welcomeScreen.SetActive(true);
        //introductionScreen.SetActive(false);
        //imageTargetDinner.SetActive(false);
        //imageTargetHarbor.SetActive(false);
        StartImageRecognition();

        StartCoroutine(EnableTextCoroutine());
    }

    public void StartImageRecognition(){
        foreach(var painting in paintings) {
            painting.SetActive(true);
            AddInfoButtonToPainting(painting);
        }
        introductionState = false;
    }

    private void AddInfoButtonToPainting(GameObject painting){
        GameObject childCopyBtn = Instantiate(infoButton);
        GameObject childCopyMenu = Instantiate(infoButtonMenu);

        childCopyBtn.transform.SetParent(painting.transform);
        childCopyMenu.transform.SetParent(painting.transform);
        childCopyBtn.SetActive(true);
        childCopyMenu.SetActive(false);
        childCopyBtn.AddComponent<InfoMenu>();
        childCopyBtn.transform.name = "InfoBtn";
        childCopyMenu.transform.name = "InfoMenuDetail";

        childCopyBtn.transform.localPosition = painting.transform.position + new Vector3(5, 0, 0);
        
    }

    private IEnumerator EnableTextCoroutine()
    {
        yield return new WaitForSeconds(delay);
        swipeText.gameObject.SetActive(true);
    }

    
}
