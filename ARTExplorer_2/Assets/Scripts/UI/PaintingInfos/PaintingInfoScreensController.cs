using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingInfoScreensController : MonoBehaviour
{
    [SerializeField]
    private GameObject _startInfoPanel;
     [SerializeField]
    private GameObject _aboutPaintingInfoPanel;
     [SerializeField]
    private GameObject _aboutInfoPanel;


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

}
