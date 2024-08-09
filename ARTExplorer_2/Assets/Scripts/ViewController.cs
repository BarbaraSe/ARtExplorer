using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;
using Unity.XR.CoreUtils;
using Unity.VisualScripting;

public class ViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _imageTargetHarbor;
    [SerializeField]
    private GameObject _imageTargetDinner;
    [SerializeField]
    private GameObject infoButton;
    [SerializeField]
    private GameObject _introductionScreens;
    [SerializeField]
    private GameObject _infoScreens;

    private GameObject[] _paintings;
    private IntroScreensView _introScreensView;

    void Start()
    {    
        _introScreensView = FindObjectOfType<IntroScreensView>();
        _paintings = new GameObject[2]{_imageTargetHarbor, _imageTargetDinner};

        _introductionScreens.SetActive(true);
        _infoScreens.SetActive(false);
        foreach (var item in _paintings)
        {
            item.SetActive(false);
        }
        //StartImageRecognition();
    }

    public void StartImageRecognition(){
        foreach(var painting in _paintings) {
            painting.SetActive(true);
            AddInfoButtonToPainting(painting);
        }
        _introScreensView.SetIntroductionScreens(false);
    }

    private void AddInfoButtonToPainting(GameObject painting){
        GameObject childCopyBtn = Instantiate(infoButton);
        GameObject infoButtonMenu = childCopyBtn.transform.Find("InfoMenuDetail").gameObject;

        childCopyBtn.transform.SetParent(painting.transform);
        childCopyBtn.SetActive(true);
        infoButtonMenu.SetActive(false);
        childCopyBtn.AddComponent<InfoMenu>();
        childCopyBtn.transform.name = "InfoBtn";
        childCopyBtn.transform.localPosition = new Vector3(0.2f, 0, 0);
    }
}
