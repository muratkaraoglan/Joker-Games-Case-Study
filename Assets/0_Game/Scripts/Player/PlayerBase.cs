using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    #region Event
    public event Action<bool> OnMoveCompleteEvent = _ => { };
    public event Action OnStepCompleteEvent = () => { };
    #endregion

    #region Serialized
    [SerializeField] protected float _moveTime = 1f;
    #endregion

    private bool _isDouble;
    #region Protected
    protected int _moveCount;
    protected Tile _currentTile;
    #endregion

    public abstract void Idle();
    public abstract void Move();

    protected virtual void Awake()
    {
        _currentTile = TileSpawner.Instance.FirstTile;
        transform.position = _currentTile.TileMovePoint.position;
        Idle();
    }

    private void OnEnable()
    {
        DiceManager.Instance.OnRollComplete += OnRollComplete;
    }

    protected void OnRollComplete(int rollResult, bool isDouble)
    {
        _moveCount = rollResult;
        _isDouble = isDouble;
        print("Move Count Before: " + _moveCount);
        TileSpawner.Instance.CheckNumberOfTurns(ref _moveCount, _isDouble);
        print("Move Count After: " + _moveCount);
        Move();
    }

    protected void OnMoveComplete()
    {
        _currentTile = _currentTile.Next;
        _currentTile.PlayInteractionAnimation();
        _currentTile.CheckFirtsTilePrize(_isDouble);
        _moveCount--;
        OnStepCompleteEvent.Invoke();
        if (_moveCount != 0) Move();
        else
        {
            Idle();
            bool hasPrize = _currentTile.GetPrize(_isDouble);
            OnMoveCompleteEvent.Invoke(hasPrize);
            _isDouble = false;
        }
    }

    protected virtual void OnDisable()
    {
        DiceManager.Instance.OnRollComplete -= OnRollComplete;
    }
}
