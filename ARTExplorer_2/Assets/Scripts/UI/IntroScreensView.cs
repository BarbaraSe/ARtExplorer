using System.Collections;
using UnityEngine;

public class IntroScreensView : MonoBehaviour
{
    [SerializeField]
    private GameObject _introductionScreens;

    [SerializeField]
    private GameObject _welcomePanel;

    [SerializeField]
    private GameObject _introductionPanel1;

    [SerializeField]
    private GameObject _introductionPanel2;
    [SerializeField]
    private GameObject _swipeText;
    private float delay = 2f;
    public bool welcome;
    
    public void StartExperience() {
        welcome = true;
        SetIntroductionScreens(true);
        SetWelcomePanelActive(true);
        SetSwipeTextActive(false);
        SetIntroductionPanel1Active(false);
        SetIntroductionPanel2Active(false);
        StartCoroutine(EnableTextCoroutine());
    }

    public GameObject GetIntroductionScreens(){
       return _introductionScreens;
    }
     public GameObject GetWelcomePanel(){
       return _welcomePanel;
    }
     public GameObject GetIntroductionPanel1(){
       return _introductionPanel1;
    }
     public GameObject GetIntroductionPanel2(){
       return _introductionPanel2;
    }
    public void SetIntroductionScreens(bool active){
        _introductionScreens.SetActive(active);
    }

    public void SetWelcomePanelActive(bool active){
        _welcomePanel.SetActive(active);
    }

     public void SetIntroductionPanel1Active(bool active){
        _introductionPanel1.SetActive(active);
    }
     public void SetIntroductionPanel2Active(bool active){
        _introductionPanel2.SetActive(active);
    }
     public void SetSwipeTextActive(bool active){
        _swipeText.SetActive(active);
    }

     private IEnumerator EnableTextCoroutine()
    {
        yield return new WaitForSeconds(delay);
        _swipeText.gameObject.SetActive(true);
    }

}
