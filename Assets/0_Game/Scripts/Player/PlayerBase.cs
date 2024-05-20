using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    public event Action OnMoveCompleteEvent = () => { };
    [SerializeField] protected float _moveTime = 1f;
    protected int _moveCount;
    protected Tile _currentTile;
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

    protected void OnRollComplete(int rollResult)
    {
        _moveCount = rollResult;
        Move();
    }

    protected void OnMoveComplete()
    {
        _currentTile = _currentTile.Next;
        _currentTile.PlayInteractionAnimation();
        _moveCount--;
        if (_moveCount != 0) Move();
        else
        {
            Idle();
            OnMoveCompleteEvent.Invoke();
            _currentTile.GetPrize();
        }
    }

    protected virtual void OnDisable()
    {
        DiceManager.Instance.OnRollComplete -= OnRollComplete;
    }
}
