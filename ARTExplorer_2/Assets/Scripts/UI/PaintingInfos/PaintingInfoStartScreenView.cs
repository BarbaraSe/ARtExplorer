using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
public class PaintingInfoStartScreenView : MonoBehaviour
{
   [SerializeField]
   private PressableButton _artistBtn;
   [SerializeField]
   private PressableButton _paintingBtn;
   [SerializeField]
   private PressableButton _eraBtn;
   [SerializeField]
   private PressableButton _significanceBtn;
   [SerializeField]
   private PressableButton _funFactsBtn;
   [SerializeField]
   private PressableButton _keyPointsBtn;
   private PaintingInfoScreensController _paintingInfoScreensController;
   // private PaintingInfoAboutPaintingView _paintingInfoAboutPaintingView;
   // private PaintingInfoPanelView _paintingInfoPanelView;
   private ViewController _viewController;
   private int index;

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

   private TMP_Text[] titles;
   private TMP_Text[] details;

   private void Start()
   {
      // titles = new TMP_Text[4] { _title1, _title2, _title3, _title4 };
      // titles = new TMP_Text[4] { _detail1, _detail2, _detail3, _detail4 };
   }

   void Update()
   {
      _paintingInfoScreensController = FindObjectOfType<PaintingInfoScreensController>();
      _viewController = GetComponent<ViewController>();
      // _paintingInfoPanelView = GetComponent<PaintingInfoPanelView>();
      // _paintingInfoAboutPaintingView = _paintingInfoScreensController.GetAboutInfo().GetComponent<PaintingInfoAboutPaintingView>();


      if (_paintingInfoScreensController.GetParentName() == "ImageTargetHarbour")
      {
         index = 0;
      }
      else if (_paintingInfoScreensController.GetParentName() == "ImageTargetDinner")
      {
         index = 1;
      }
      else
      {
         index = 0;
      }


   }

   public void ShowArtistInfo()
   {
      if (_paintingInfoScreensController._paintingInfos[index].AboutArtist != null)
      {
         _paintingInfoScreensController.SetStartScreenInfoActive(false);
         _paintingInfoScreensController.SetAboutInfoActive(true);

         List<DetailItem> artistInfo = _paintingInfoScreensController._paintingInfos[index].AboutArtist;
         // _paintingInfoPanelView.SetText(artistInfo);
         SetTextInInfoPanel(artistInfo);
         SetInfoPanelHeader("About the Artist");
      }
   }

   public void ShowPaintingInfo()
   {
      _paintingInfoScreensController.SetStartScreenInfoActive(false);
      _paintingInfoScreensController.SetPaintingInfoActive(true);

      //  _paintingInfoAboutPaintingView.SetTextAboutPainting(_paintingInfoScreensController._paintingInfos[index].AboutPainting);
      SetTextAboutPainting(_paintingInfoScreensController._paintingInfos[index].AboutPainting);
   }
   public void ShowEraInfo()
   {
      if (_paintingInfoScreensController._paintingInfos[index].AboutEra != null)
      {
         _paintingInfoScreensController.SetStartScreenInfoActive(false);
         _paintingInfoScreensController.SetAboutInfoActive(true);
         // _paintingInfoPanelView = FindObjectOfType<PaintingInfoPanelView>();
         SetTextInInfoPanel(_paintingInfoScreensController._paintingInfos[index].AboutEra);
         SetInfoPanelHeader("About Era");
      }
   }
   public void ShowSignificanceInfo()
   {
      if (_paintingInfoScreensController._paintingInfos[index].Significance != null)
      {
         _paintingInfoScreensController.SetStartScreenInfoActive(false);
         _paintingInfoScreensController.SetAboutInfoActive(true);
         // _paintingInfoPanelView = FindObjectOfType<PaintingInfoPanelView>();
         SetTextAboutSignificance(_paintingInfoScreensController._paintingInfos[index].Significance);
      }
   }
   public void ShowFunFactsInfo()
   {
      if (_paintingInfoScreensController._paintingInfos[index].FunFacts != null)
      {
         _paintingInfoScreensController.SetStartScreenInfoActive(false);
         _paintingInfoScreensController.SetAboutInfoActive(true);
         // _paintingInfoPanelView = FindObjectOfType<PaintingInfoPanelView>();
         SetTextInInfoPanel(_paintingInfoScreensController._paintingInfos[index].FunFacts);
         SetInfoPanelHeader("Fun Facts");
      }
   }
   public void ShowKeyPointsInfo()
   {
      if (_paintingInfoScreensController._paintingInfos[index].KeyPointsEra != null)
      {
         _paintingInfoScreensController.SetStartScreenInfoActive(false);
         _paintingInfoScreensController.SetAboutInfoActive(true);
         // _paintingInfoPanelView = FindObjectOfType<PaintingInfoPanelView>();
         SetTextInInfoPanel(_paintingInfoScreensController._paintingInfos[index].KeyPointsEra);
         SetInfoPanelHeader("Key Points about Era");
      }
   }
   public void SetTextAboutPainting(AboutPaintingItem list)
   {
      Debug.Log($"{list.Title}: {list.Artist}: {list.Medium}: {list.Date}");

      _title.text = list.Title;
      _artist.text = list.Artist;
      _medium.text = list.Medium;
      _date.text = list.Date;

   }

   // Info General Panel
   public void SetTextAboutSignificance(string str)
   {
      _title1.text = "Significance";
      _detail1.text = str;
      for (int i = 1; i < titles.Length; i++)
      {
         titles[i].enabled = false;
         details[i].enabled = false;
         _infoPanelTitle.enabled = false;
      }
   }

   public void SetAllItemsFalse()
   {
      for (int i = 1; i < titles.Length; i++)
      {
         titles[i].enabled = false;
         details[i].enabled = false;
      }
   }

   public void SetInfoPanelHeader(string text)
   {
      _infoPanelTitle.text = text;
   }

   public void SetTextInInfoPanel(List<DetailItem> list)
   {

      titles = new TMP_Text[3] { _title1, _title2, _title3 };
      details = new TMP_Text[3] { _detail1, _detail2, _detail3 };
      SetAllItemsFalse();
      int idx = 0;
      foreach (var item in list)
      {
         Debug.Log($"count item list {list.Count}");
         Debug.Log($"{item.Title}: {item.Detail}");

         if (list.Count == 0)
         {
            Debug.LogWarning("No Information available");
            SetInfoPanelHeader("No Information available");
         }
         // else if (list.Count == 1)
         // {
         //    _title1.text = item.Title;
         //    _detail1.text = item.Title;
         // }
         else// if (list.Count > 1)
         {

            if (idx < 3)
            {
               titles[idx].text = item.Title;
               details[idx].text = item.Detail;
               titles[idx].enabled = true;
               details[idx].enabled = true;
               idx += 1;
            }
         }
      }

   }
}
