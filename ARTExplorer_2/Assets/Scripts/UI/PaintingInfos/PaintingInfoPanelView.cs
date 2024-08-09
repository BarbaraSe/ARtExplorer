using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaintingInfoPanelView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _significance;
    [SerializeField]
    private GameObject _panel;
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

    private PaintingInfoScreensController _paintingInfoScreensController;
    private TMP_Text[] titles;
    private TMP_Text[] details;


    private void Start() {
        titles = new TMP_Text[4]{_title1, _title2, _title3, _title4};
        titles = new TMP_Text[4]{_detail1, _detail2, _detail3, _detail4};
    }

    void Update() {
        _paintingInfoScreensController = FindObjectOfType<PaintingInfoScreensController>();
    }

    public void SetTextAboutSignificance(string str)
    {
       _title1.text = "Significance";
       _detail1.text = str;
       for (int i = 1; i < titles.Length; i++)
       {
            titles[i].enabled = false;
            details[i].enabled  = false;
            _infoPanelTitle.enabled = false;
       }
    }

    public void SetInfoPanelHeader(string text){
        _infoPanelTitle.text = text;
    }

    public void SetText(List<DetailItem> list)
    {
       int idx = 0;
       foreach (var item in list)
        {
            if (idx <4) {
                titles[idx].text = item.Title;
                details[idx].text = item.Detail;
                idx += 1;
            }
            
        }
    }
}
