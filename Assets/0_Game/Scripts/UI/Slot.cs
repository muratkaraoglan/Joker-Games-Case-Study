using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemCountText;

    [Header("Floating Text")]
    [SerializeField] private Animator _floatingTextAnimator;
    [SerializeField] private RectTransform _floatingTextParentTransform;
    [SerializeField] private Image _floatingTextImage;
    [SerializeField] private TextMeshProUGUI _floatingText;

    private float _screenWidthHalf = Screen.width * .5f;
    private void Start()
    {
        _floatingTextParentTransform.parent = transform.root;
        _floatingTextParentTransform.anchoredPosition = Vector3.zero;

    }
    public void Init(Sprite itemImage, int itemCount)
    {
        _itemImage.sprite = itemImage;
        _floatingTextImage.sprite = itemImage;
        _itemCountText.SetText("x" + itemCount);
    }

    public void UptadeItemCount(int itemCount, int amount)
    {
        _itemCountText.text = "x" + itemCount;
        _floatingText.text = amount.ToString();
        _floatingTextParentTransform.gameObject.SetActive(true);
        _floatingTextParentTransform.anchoredPosition = new Vector3(Random.Range(-_screenWidthHalf + 100f, _screenWidthHalf - 100f), Random.Range(-200f, 0f), 0);
        _floatingTextAnimator.Play("FloatingText", -1, 0f);
        //Tween moveTween = new MoveTween(_floatingTextParentTransform, _floatingTextParentTransform.position, _floatingTextParentTransform.position + Vector3.up * 100, 1f, Easing.Linear, ResetFloatingText);
    }

    private void ResetFloatingText()
    {
        _floatingTextParentTransform.gameObject.SetActive(false);
        _floatingTextParentTransform.anchoredPosition = Vector3.zero;
    }
}
