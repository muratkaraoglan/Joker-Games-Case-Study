using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    protected int _moveCount;
    protected Tile _currentTile;
    public abstract void Idle();
    public abstract void Move();

    protected virtual void Awake()
    {
        DiceManager.Instance.OnRollComplete += OnRollComplete;
        _currentTile = FindObjectOfType<TileSpawner>().FirstTile;
        transform.position = _currentTile.TileMovePoint.position;
        Idle();
    }

    protected void OnRollComplete(int rollResult)
    {
        _moveCount = rollResult;
        Move();
    }

    protected void OnMoveComplete()
    {
        _currentTile = _currentTile.Next;
        _currentTile.GetPrize();
        _moveCount--;
        if (_moveCount != 0) Move();
        else
        {
            Idle();
            //Roll butonunu ac
        }
    }

    protected virtual void OnDisable()
    {
        DiceManager.Instance.OnRollComplete -= OnRollComplete;
    }
}
