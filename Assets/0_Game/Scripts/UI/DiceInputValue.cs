using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiceInputValue : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [HideInInspector] public ScrollRect scrollRect;
    public TextMeshProUGUI _placeHolderText;
    [SerializeField] private TextMeshProUGUI _inputText;


    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        scrollRect.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.OnEndDrag(eventData);
    }

    public int InputValue
    {
        get
        {
            int value = _inputText.text[0] - '0';
            value = value > 6 ? -1 : value;
            return value;
        }
    }
}