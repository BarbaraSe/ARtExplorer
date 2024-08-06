using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonController : MonoBehaviour
{
    
    public GameObject uiPanel;
    public void InfoBtnPressed(){

    }

    // removes 3D objects shows only painting
    public void TwoDBtnPressed(){

    }

    // shows 3D objects
    public void ThreeDBtnPressed(){

    }
  
    public void UndoPositionBtnPressed(){

    }

    public void UndoRotationBtnPressed(){
        
    }
    

    public void UndoScaleBtnPressed(){
            
    }

    public void UndoObjectAllBtnPressed(){
            
    }

    public void UndoAllBtnPressed(){
        
    }

    public void CancelBtnPressed(){
        uiPanel.SetActive(false);
    }
}
