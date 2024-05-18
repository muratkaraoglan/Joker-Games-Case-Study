using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    #region Serialized
    [SerializeField] private Image _tileImage;
    [SerializeField] private TextMeshProUGUI _amountText;
    #endregion

    #region Private
    private Tile _nextTile;
    private bool _isEmpty;
    private int _amount;
    #endregion
    public void Init(TileTypeHolder tileTypeHolder,Vector3 direction)
    {
        transform.forward = direction;
        if (tileTypeHolder.Type == TileType.Empty)
        {
            _isEmpty = true;
        }
        else
        {
            _tileImage.sprite = tileTypeHolder.TileSprite;
            _tileImage.enabled = true;
            _amount = Random.Range(tileTypeHolder.MinAmount, tileTypeHolder.MaxAmount + 1);
            _amountText.SetText(GameManager.Instance.GetColoredString(_amount));
        }
    }

    public void SetNextTile(Tile nextTile)
    {
        _nextTile = nextTile;
    }
}
