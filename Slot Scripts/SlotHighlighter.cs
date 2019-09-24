using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverAlpha = 1.0f;
    private Color originalColor;
    private Image imageComponent;

    void Start()
    {
        imageComponent = gameObject.GetComponent<Image>();
        originalColor = imageComponent.color;
    }

    public void OnPointerEnter(PointerEventData eventData){
        imageComponent.color = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, hoverAlpha);
    }
    public void OnPointerExit(PointerEventData eventData){
        imageComponent.color = originalColor;
    }
}
