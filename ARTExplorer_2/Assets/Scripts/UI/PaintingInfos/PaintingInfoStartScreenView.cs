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
      
   }

   public void ShowPaintingInfo(){
    _paintingInfoScreensController.SetAboutInfoActive(true);
    List<AboutPaintingItem> artistInfo = _paintingInfoScreensController._paintingInfos[index].AboutPainting;
    _paintingInfoAboutPaintingView.SetTextAboutPainting(artistInfo);
   }
   public void ShowEraInfo(){
    
   }
   public void ShowSignificanceInfo(){
    
   }
   public void ShowFunFactsInfo(){
    
   }
   public void ShowKeyPointsInfo(){
    
   }
}
