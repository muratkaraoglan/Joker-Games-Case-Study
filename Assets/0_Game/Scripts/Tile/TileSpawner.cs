using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(-9)]
public class TileSpawner : Singelton<TileSpawner>
{
    #region Event
    public event Action OnTileOrderFinished = () => { };
    #endregion

    #region Serialized
    [SerializeField] private TileTypeScriptableObject _tileConfig;
    [SerializeField] private float _tileSpawnMoveTargetTime = .3f;
    #endregion

    #region Private
    private int _tileCount = 0;
    private Tile firstTile = null;
    private Vector3[] spawnDirections = new Vector3[]
    {
        Vector3.forward,
        Vector3.right,
        Vector3.back,
        Vector3.left };
    private Dictionary<TileType, int> _fullBoardValue = new Dictionary<TileType, int>();

    #endregion

    private IEnumerator Start()
    {
        Vector3 targetPosition = Vector3.zero;
        Vector3 spawnOffset = Vector3.zero;
        Tile backTile = null;


        for (int i = 0; i < spawnDirections.Length; i++)
        {
            for (int j = 0; j < _tileConfig.TileCountOnEdge; j++)
            {
                _tileCount++;
                targetPosition = spawnDirections[i] * j * _tileConfig.TileSpawnOffset + spawnOffset;

                Tile tile = Instantiate(_tileConfig.TilePrefab, targetPosition + Vector3.up * 2f, Quaternion.identity);
                tile.name = _tileCount.ToString();
                Vector3 lookDirection = Quaternion.Euler(0, 90, 0) * spawnDirections[i];

                bool isFirstItemInRow = j == 0;
                if (isFirstItemInRow) lookDirection = Quaternion.Euler(0, 45, 0) * spawnDirections[i];

                var (tileType, amount) = tile.Init(_tileConfig.GetRandomTile(), lookDirection, _tileCount, isFirstItemInRow);

                if (_fullBoardValue.TryGetValue(tileType, out int value))
                {
                    _fullBoardValue[tileType] += amount;
                }
                else
                {
                    _fullBoardValue.Add(tileType, amount);
                }

                Tween move = new MoveTween(tile.transform, tile.transform.position, targetPosition, _tileSpawnMoveTargetTime, Easing.Linear);

                if (backTile != null)
                {
                    backTile.SetNextTile(tile);
                }
                else
                {
                    firstTile = tile;
                }
                backTile = tile;
                yield return new WaitForSeconds(.1f);
            }

            spawnOffset = targetPosition + spawnDirections[i] * _tileConfig.TileSpawnOffset;
        }

        _fullBoardValue.Remove(TileType.Empty);
        for (int i = 0; i < _tileConfig.TileTypeHolderList.Count; i++)
        {
            if (_tileConfig.TileTypeHolderList[i].Type == TileType.Empty) continue;
            _fullBoardValue[_tileConfig.TileTypeHolderList[i].Type] += _tileConfig.BeginningTilePrize;
        }

        backTile.SetNextTile(firstTile);
        OnTileOrderFinished.Invoke();
    }

    public Tile FirstTile => firstTile;
    public TileTypeScriptableObject TileConfig => _tileConfig;

    public void CheckNumberOfTurns(ref int moveCount, bool isDouble)
    {
        int turnCount = moveCount / _tileCount;
        int doubleRate = isDouble ? 2 : 1;
        if (turnCount > 0)
        {
            foreach (KeyValuePair<TileType, int> keyValuePair in _fullBoardValue)
            {
                DataManager.Instance.UpdateTileData(keyValuePair.Key, keyValuePair.Value * doubleRate);
            }
        }

        moveCount = moveCount % _tileCount;
    }

}
