using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class ShowShipOnClick : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject shipObject;

    void Start()
    {
        if (shipObject != null)
        {
            shipObject.SetActive(false);
        }
        else
        {
            Debug.LogError("shipObject is not assigned.");
        }
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        if (shipObject != null)
        {
            shipObject.SetActive(!shipObject.activeSelf);
            Debug.Log("Ship visibility toggled.");
        }
        else
        {
            Debug.LogError("shipObject is not assigned.");
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
}
