using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

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
   private PaintingInfoAboutPaintingView _paintingInfoAboutPaintingView;
   private PaintingInfoPanelView _paintingInfoPanelView;
   private int index;

   void Update() {
      _paintingInfoScreensController = FindObjectOfType<PaintingInfoScreensController>();
      _paintingInfoAboutPaintingView = FindObjectOfType<PaintingInfoAboutPaintingView>();
      _paintingInfoPanelView = FindObjectOfType<PaintingInfoPanelView>();
      if (_paintingInfoScreensController._parentName == "ImageTargetHarbour") {
         index = 0;
      } else if (_paintingInfoScreensController._parentName == "ImageTargetDinner") {
         index = 1;
      }
   }

   public void ShowArtistInfo(){
      if (_paintingInfoScreensController._paintingInfos[index].AboutArtist != null) {
         _paintingInfoScreensController.SetStartInfoPanelActive(false);
         _paintingInfoScreensController.SetPaintingInfoActive(true);
         List<DetailItem> artistInfo = _paintingInfoScreensController._paintingInfos[index].AboutArtist;
         _paintingInfoPanelView.SetText(artistInfo);
         _paintingInfoPanelView.SetInfoPanelHeader("About Artist");
      }
   }

   public void ShowPaintingInfo(){
      _paintingInfoScreensController.SetStartInfoPanelActive(false);
      _paintingInfoScreensController.SetAboutInfoActive(true);
      List<AboutPaintingItem> artistInfo = _paintingInfoScreensController._paintingInfos[index].AboutPainting;
      _paintingInfoAboutPaintingView.SetTextAboutPainting(artistInfo);
   }
   public void ShowEraInfo(){
       if (_paintingInfoScreensController._paintingInfos[index].AboutEra != null) {
         _paintingInfoScreensController.SetStartInfoPanelActive(false);
         _paintingInfoScreensController.SetPaintingInfoActive(true);
         List<DetailItem> artistInfo = _paintingInfoScreensController._paintingInfos[index].AboutEra;
         _paintingInfoPanelView.SetText(artistInfo);
         _paintingInfoPanelView.SetInfoPanelHeader("About Era");
       }
   }
   public void ShowSignificanceInfo(){
      if (_paintingInfoScreensController._paintingInfos[index].Significance != null) {
         _paintingInfoScreensController.SetStartInfoPanelActive(false);
         _paintingInfoScreensController.SetPaintingInfoActive(true);
         string artistInfo = _paintingInfoScreensController._paintingInfos[index].Significance;
         _paintingInfoPanelView.SetTextAboutSignificance(artistInfo);
      }
   }
   public void ShowFunFactsInfo(){
      if (_paintingInfoScreensController._paintingInfos[index].FunFacts != null) {
         _paintingInfoScreensController.SetStartInfoPanelActive(false);
         _paintingInfoScreensController.SetPaintingInfoActive(true);
         List<DetailItem> artistInfo = _paintingInfoScreensController._paintingInfos[index].FunFacts;
         _paintingInfoPanelView.SetText(artistInfo);
         _paintingInfoPanelView.SetInfoPanelHeader("Fun Facts");
      }
   }
   public void ShowKeyPointsInfo(){
      if (_paintingInfoScreensController._paintingInfos[index].KeyPointsEra != null) {
         _paintingInfoScreensController.SetStartInfoPanelActive(false);
         _paintingInfoScreensController.SetPaintingInfoActive(true);
         List<DetailItem> artistInfo = _paintingInfoScreensController._paintingInfos[index].KeyPointsEra;
         _paintingInfoPanelView.SetText(artistInfo);
         _paintingInfoPanelView.SetInfoPanelHeader("Key Points about Era");
      }
   }
}
