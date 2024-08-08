using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InfoMenu : MonoBehaviour
{
    private GameObject _menuDetail;
    private string _parentName;
    private GameObject _parent;
    private UIMenuController _uIMenuController;
    private Vector3 _offsetRight = new Vector3(5, 0, 0);
    private Vector3 _offsetAbove = new Vector3(0, 10, 0);
    public List<PaintingInfo> _paintingInfos;
    private InfoMenuView _infoMenuView;

    void Start()
    {
        _parent = gameObject.transform.parent.gameObject;
        _parentName = _parent.name;
        _menuDetail = _parent.transform.Find("InfoMenuDetail").gameObject;
        _uIMenuController = FindObjectOfType<UIMenuController>();
        _infoMenuView = FindObjectOfType<InfoMenuView>();
        LoadJSON();
        DisplayInfo(_paintingInfos[1]);
    }

    public void OpenInfoDetailBtnPressed()
    {
        if (_menuDetail.activeSelf)
        {
            _menuDetail.SetActive(false);
        }
        else
        {
            _menuDetail.SetActive(true);
            PositionPanel(_offsetRight, _menuDetail);
        }
    }

    private void PositionPanel(Vector3 offset, GameObject panel)
    {
        if (_parent != null && panel != null)
        {
            panel.transform.position = _parent.transform.position + offset;
        }
    }

    public void GetGeneralInformationBtnPressed()
    {
        GameObject introductionScreen = GameObject.Find("IntroductionScreen");
        introductionScreen.SetActive(true);
    }

    public void GetPaintingInformationBtnPressed()
    {
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
