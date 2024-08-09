using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PaintingInfoScreensController : MonoBehaviour
{
    [SerializeField]
    private GameObject _startInfoPanel;
     [SerializeField]
    private GameObject _aboutPaintingInfoPanel;
     [SerializeField]
    private GameObject _aboutInfoPanel;
    public List<PaintingInfo> _paintingInfos;
    public string _parentName;
    private InfoMenu _infoMenu;

    void Start() {
        LoadJSON();
    }

    void Update()
    {
          _infoMenu = FindObjectOfType<InfoMenu>();
         _parentName = _infoMenu.GetParentName();
    }


    public void SetStartInfoPanelActive(bool active){
        _startInfoPanel.SetActive(active);
    }
    public void SetPaintingInfoActive(bool active){
        _aboutPaintingInfoPanel.SetActive(active);
    }
    public void SetAboutInfoActive(bool active){
        _aboutInfoPanel.SetActive(active);
    }
    public bool GetStartInfoPanelActiveStatus(){
        return _startInfoPanel.activeSelf;
    }
    public bool GetPaintingInfoActiveStatus(){
        return _aboutPaintingInfoPanel.activeSelf;
    }
    public bool GetAboutInfoActiveStatus(){
        return _aboutInfoPanel.activeSelf;
    }

    // public void GetDetailedPaintingInformation()
    //{
    //    if (_parentName.Contains("Dinner"))
   //     {
    //        _viewController._infoScreens.SetActive(true);
    //        //DisplayInfo(_paintingInfos[1]);
            //infoMenuView.SetTextAboutPainting(_paintingInfos[1].AboutPainting);
//
   //     }
   //     else if (_parentName.Contains("Harbour"))
    //    {
    //        DisplayInfo(_paintingInfos[0]);
    //    }
   //     else
    //    {
   //         Debug.Log("No Information about this painting available");
    //    }
    //}

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

    

}
