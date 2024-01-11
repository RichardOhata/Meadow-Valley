using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        float r = 2 / 255f;
        float g = 65 / 255f;
        float b = 5 / 255f;
        Color targetColor = new Color(r, g, b);

        eventData.pointerEnter.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = targetColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerEnter.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
    }
}
