using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaintingInfoAboutPaintingView : MonoBehaviour
{
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

    public bool GetPanelActiveState(){
        return _aboutPainting.activeSelf;
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
        gameObject.SetActive(true);


    }
}
