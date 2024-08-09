using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Microsoft.MixedReality.Toolkit.UI;

public class InfoMenu : MonoBehaviour
{
    private GameObject _menuDetail;
    private string _parentName;
    private GameObject _parent;
    private UIMenuController _uIMenuController;
    private ViewController _viewController;

    void Start()
    {
        // gameObject is infoBtn
        _parent = gameObject.transform.parent.gameObject;
        _parentName = _parent.name;
        _menuDetail = gameObject.transform.Find("InfoMenuDetail").gameObject;
        _uIMenuController = FindObjectOfType<UIMenuController>();
        _viewController = FindObjectOfType<ViewController>();
        //DisplayInfo(_paintingInfos[1]);
        AddButtonListeners();
    }

    public string GetParentName(){
        return _parentName;
    }

    public void AddButtonListeners(){
        PressableButton[] buttons = _menuDetail.transform.GetComponentsInChildren<PressableButton>();
        foreach (var item in buttons)
        {
        Debug.Log(item.name);
        if (item.name == "GeneralInfo") {
            item.ButtonPressed.AddListener(GetGeneralInformation);
        } else if (item.name == "PaintingInfo") {
            item.ButtonPressed.AddListener(_viewController.SetInfoScreensActive);
        } else if (item.name == "Hide3DBtn") {
            item.ButtonPressed.AddListener(_uIMenuController.ObjectVisibilityButton);
        }
        }

        if ( gameObject.GetComponent<PressableButton>() != null)
        {
            gameObject.GetComponent<PressableButton>().ButtonPressed.AddListener(OpenInfoDetailMenu);
        }
        else
        {
            Debug.LogError("Info button component not found!");
        }
    }

    public void OpenInfoDetailMenu()
    {
        if (_menuDetail.activeSelf)
        {
            _menuDetail.SetActive(false);
            Debug.Log("Info Detail Menu not visible");
        }
        else
        {
            _menuDetail.SetActive(true);
            Debug.Log("Info Detail Menu visible");
        }
    }

    public void GetGeneralInformation()
    {
        IntroScreensView introductionScreen = GameObject.Find("IntroductionScreens").GetComponent<IntroScreensView>();
        introductionScreen.SetIntroductionScreens(true);
        introductionScreen.SetIntroductionPanel1Active(true);
    }



    

   
}
