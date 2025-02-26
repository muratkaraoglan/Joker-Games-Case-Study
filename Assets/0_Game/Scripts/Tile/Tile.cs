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
    private ParticleSystem _collectParticle;
    private TileType _type;
    private Tile _nextTile;
    private bool _isEmpty;
    private int _id;
    private int _amount;
    #endregion
    public (TileType, int) Init(TileTypeHolder tileTypeHolder, Vector3 direction, int id, bool isFirstTileInRow)
    {
        _canvasParent.forward = direction;
        _indexText.SetText(id.ToString());
        _id = id;
        _animator = GetComponent<Animator>();

        if (isFirstTileInRow) _indexText.rectTransform.anchoredPosition = new Vector2(-.8f, .3f);

        if (tileTypeHolder.Type == TileType.Empty || id == 1)
        {
            _isEmpty = true;
            if (_id == 1)
            {
                _collectParticle = Instantiate(TileSpawner.Instance.TileConfig.BeginningTileParticle, transform);
                _amount = TileSpawner.Instance.TileConfig.BeginningTilePrize;

            }
            return (TileType.Empty, 0);
        }
        else
        {
            _type = tileTypeHolder.Type;
            _tileImage.sprite = tileTypeHolder.TileSprite;
            _tileImage.enabled = true;
            _amount = Random.Range(tileTypeHolder.MinAmount, tileTypeHolder.MaxAmount + 1);
            _amountText.SetText(GameManager.Instance.GetColoredString(_amount));
            _collectParticle = Instantiate(tileTypeHolder.TileCollectParticlePrefab, transform);

            return (_type, _amount);
        }
    }

    public void Reset()
    {
        _tileImage.sprite = null;
        _tileImage.enabled = false;
        _amountText.SetText("");
        _isEmpty = true;
        _collectParticle = null;
    }

    public void SetNextTile(Tile nextTile)
    {
        _nextTile = nextTile;
    }

    public Tile Next => _nextTile;

    public bool GetPrize(bool isDouble)
    {
        if (_isEmpty) return false;
        _collectParticle.Play();
        DataManager.Instance.UpdateTileData(_type, isDouble ? _amount * 2 : _amount);
        return true;
    }

    public void PlayInteractionAnimation()
    {
        _animator.Play("Interaction", -1, 0f);
    }

    public void CheckFirtsTilePrize(bool isDouble)
    {
        if (_id == 1)
        {
            int prize = isDouble ? TileSpawner.Instance.TileConfig.BeginningTilePrize * 2 : TileSpawner.Instance.TileConfig.BeginningTilePrize;
            _collectParticle.Play();

            for (int i = 0; i < TileSpawner.Instance.TileConfig.TileTypeHolderList.Count; i++)
            {
                if (TileSpawner.Instance.TileConfig.TileTypeHolderList[i].Type == TileType.Empty) continue;
                DataManager.Instance.UpdateTileData(TileSpawner.Instance.TileConfig.TileTypeHolderList[i].Type, prize);
            }
        }
    }
}
