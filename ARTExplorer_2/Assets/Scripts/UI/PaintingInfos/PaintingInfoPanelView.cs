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

    public void SetTextAboutSignificance(string str)
    {

    }

    public void SetText(List<DetailItem> list)
    {
       _panel.SetActive(true);
       foreach (var item in list)
        {

        }
    }

}
