using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    #region Serialized
    [field: SerializeField] public Transform TileMovePoint { get; private set; }
    [SerializeField] private Transform _canvasParent;
    [SerializeField] private Image _tileImage;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private TextMeshProUGUI _indexText;
    #endregion

    #region Private
    private Animator _animator;
    private TileType _type;
    private Tile _nextTile;
    private bool _isEmpty;
    private int _amount;
    #endregion
    public void Init(TileTypeHolder tileTypeHolder, Vector3 direction, int id, bool isFirstTileInRow)
    {
        _canvasParent.forward = direction;
        _indexText.SetText(id.ToString());
        _type = tileTypeHolder.Type;
        if (tileTypeHolder.Type == TileType.Empty)
        {
            _isEmpty = true;
        }
        else
        {
            _tileImage.sprite = tileTypeHolder.TileSprite;
            _tileImage.enabled = true;
            if (isFirstTileInRow) _indexText.rectTransform.anchoredPosition = new Vector2(-.8f, .3f);
            _amount = Random.Range(tileTypeHolder.MinAmount, tileTypeHolder.MaxAmount + 1);
            _amountText.SetText(GameManager.Instance.GetColoredString(_amount));
        }
        _animator = GetComponent<Animator>();
    }

    public void Reset()
    {
        _tileImage.sprite = null;
        _tileImage.enabled = false;
        _amountText.SetText("");
        _isEmpty = true;
    }

    public void SetNextTile(Tile nextTile)
    {
        _nextTile = nextTile;
    }

    public Tile Next => _nextTile;

    public void GetPrize()
    {
      
        if (_isEmpty) return;
        DataManager.Instance.UpdateTileData(_type, _amount);
    }

    public void PlayInteractionAnimation()
    {
        _animator.Play("Interaction", -1, 0f);
    }
}
