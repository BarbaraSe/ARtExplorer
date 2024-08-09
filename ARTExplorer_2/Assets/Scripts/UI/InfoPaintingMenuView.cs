using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPaintingMenuView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _significance;
    [SerializeField]
    private GameObject _aboutPainting;
    [SerializeField]
    private TMP_Text _title;
    [SerializeField]
    private TMP_Text _artist;
    [SerializeField]
    private TMP_Text _medium;
    [SerializeField]
    private TMP_Text _date;
    [SerializeField]
    private GameObject _infoPanel;
    [SerializeField]
    private TMP_Text _infoPanelTitle;
    [SerializeField]
    private TMP_Text _title1;
    [SerializeField]
    private TMP_Text _detail1;
    [SerializeField]
    private TMP_Text _title2;
    [SerializeField]
    private TMP_Text _detail2;
    [SerializeField]
    private TMP_Text _title3;
    [SerializeField]
    private TMP_Text _detail3;
    [SerializeField]
    private TMP_Text _title4;
    [SerializeField]
    private TMP_Text _detail4;

    
    public bool InfoPanelState {set; get;}

    void Start()
    {
        InfoPanelState = true;
    }


    public void SetTextAboutSignificance(string str)
    {

    }
    public void SetTextAboutPainting(List<AboutPaintingItem> aboutPaintingItem)
    {
        _aboutPainting.SetActive(true);
        foreach (var item in aboutPaintingItem)
        {
            _title.text = item.Title;
            _artist.text = item.Artist;
            _medium.text = item.Medium;
            _date.text = item.Date;
        }
    }

    public void SetTextAboutArtist(List<DetailItem> list)
    {
        _infoPanel.SetActive(true);


    }

    public void SetTextAboutEra(List<DetailItem> list)
    {

    }

    public void SetTextAboutFunFacts(List<DetailItem> list)
    {

    }

    public void SetTextAboutKeyPointsEra(List<DetailItem> list)
    {

    }

}
