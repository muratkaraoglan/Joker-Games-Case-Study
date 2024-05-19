using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemCountText;

    public void Init(Sprite itemImage, int itemCount)
    {
        _itemImage.sprite = itemImage;
        _itemCountText.SetText("x" + itemCount);
    }

    public void UptadeItemCount(int itemCount) => _itemCountText.text = "x" + itemCount;
}
