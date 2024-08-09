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
    public List<PaintingInfo> _paintingInfos;
    private InfoPaintingMenuView _infoMenuView;
    private ViewController _viewController;

    void Start()
    {
        // gameObject is infoBtn
        _parent = gameObject.transform.parent.gameObject;
        _parentName = _parent.name;
        _menuDetail = gameObject.transform.Find("InfoMenuDetail").gameObject;
        _uIMenuController = FindObjectOfType<UIMenuController>();
        _infoMenuView = FindObjectOfType<InfoPaintingMenuView>();
        _viewController = FindObjectOfType<ViewController>();
        LoadJSON();
        //DisplayInfo(_paintingInfos[1]);
        AddButtonListeners();
    }

    public void AddButtonListeners(){
        PressableButton[] buttons = _menuDetail.transform.GetComponentsInChildren<PressableButton>();
        foreach (var item in buttons)
        {
        Debug.Log(item.name);
        if (item.name == "GeneralInfo") {
            item.ButtonPressed.AddListener(GetGeneralInformation);
        } else if (item.name == "PaintingInfo") {
            item.ButtonPressed.AddListener(GetDetailedPaintingInformation);
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
        Debug.Log("Generel Info Button pressed");
        IntroScreensView introductionScreen = GameObject.Find("IntroductionScreens").GetComponent<IntroScreensView>();
        introductionScreen.SetIntroductionScreens(true);
        introductionScreen.SetIntroductionPanel1Active(true);
    }

    public void GetDetailedPaintingInformation()
    {
        Debug.Log("Detailed Info Button pressed");
        if (_parentName.Contains("Dinner"))
        {
            DisplayInfo(_paintingInfos[1]);
            _infoMenuView.SetTextAboutPainting(_paintingInfos[1].AboutPainting);

        }
        else if (_parentName.Contains("Harbour"))
        {
            DisplayInfo(_paintingInfos[0]);
        }
        else
        {
            Debug.Log("No Information about this painting available");
        }
    }

    public void LoadJSON()
    {
        string path = Application.streamingAssetsPath + "/PaintingInfos.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _paintingInfos = JsonUtility.FromJson<Wrapper>(WrapArray(json)).Paintings;
        }
        else
        {
            Debug.LogError("JSON file not found!");
        }
    }

    private string WrapArray(string jsonArray)
    {
        return "{\"Paintings\":" + jsonArray + "}";
    }

    void DisplayInfo(PaintingInfo painting)
    {

        Debug.Log($"Artist: {painting.Significance}");

        foreach (var item in painting.AboutPainting)
        {
            Debug.Log($"{item.Title}: {item.Artist}: {item.Medium}: {item.Date}");
        }

        foreach (var item in painting.AboutArtist)
        {
            Debug.Log($"{item.Title}: {item.Detail}");
        }

        foreach (var item in painting.AboutEra)
        {
            Debug.Log($"{item.Title}: {item.Detail}");
        }

        foreach (var item in painting.FunFacts)
        {
            Debug.Log($"{item.Title}: {item.Detail}");
        }

    }
}
