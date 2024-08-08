using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMenu : MonoBehaviour
{
    public GameObject panelDetail;
    public void OpenInfoDetailBtnPressed()
    {
        if(panelDetail.activeSelf) {
            panelDetail.SetActive(false);
        } else {
            panelDetail.SetActive(true);
            PositionPanel(new Vector2(5, 0));
        }
    }
  
    private void PositionPanel(Vector2 offset)
    {
        if (gameObject != null && panelDetail != null)
        {
            panelDetail.transform.position = gameObject.transform.position + (Vector3)offset;
        }
    }
}
