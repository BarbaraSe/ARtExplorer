using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class PaintingInfoScreensController : MonoBehaviour
{
    [SerializeField]
    private GameObject _startScreenInfoPanel;
    [SerializeField]
    private GameObject _aboutPaintingInfoPanel;
    [SerializeField]
    private GameObject _aboutInfoPanel;
    private string _parentName;
    public List<PaintingInfo> _paintingInfos;
    private InfoDetailMenu _infoMenu;

    void Start()
    {
        LoadJSON();
    }

    void Update()
    {
        _infoMenu = FindObjectOfType<InfoDetailMenu>();
        _parentName = _infoMenu.GetParentName();
    }

    public string GetParentName()
    {
        return _parentName;
    }

    public void SetStartScreenInfoActive(bool active)
    {
        _startScreenInfoPanel.SetActive(active);
    }
    public void SetPaintingInfoActive(bool active)
    {
        _aboutPaintingInfoPanel.SetActive(active);
    }
    public void SetAboutInfoActive(bool active)
    {
        _aboutInfoPanel.SetActive(active);
    }
    public GameObject GetStartScreenInfoPanel()
    {
        return _startScreenInfoPanel;
    }
    public GameObject GetPaintingInfo()
    {
        return _aboutPaintingInfoPanel;
    }
    public GameObject GetAboutInfo()
    {
        return _aboutInfoPanel;
    }

    public void LoadJSON()
    {
        string path = Application.streamingAssetsPath + "/PaintingInfos.json";

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            _paintingInfos = JsonConvert.DeserializeObject<List<PaintingInfo>>(jsonString);
        }
        else
        {
            Debug.LogError("JSON file not found!");
        }
    }
}
