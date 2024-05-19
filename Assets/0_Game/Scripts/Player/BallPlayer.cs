using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPlayer : PlayerBase
{
    [SerializeField] private Animator _animator;
    [SerializeField, Min(0.1f)] private float _jumpTime = .5f;
    public override void Idle()
    {
        _animator.enabled = true;
    }

    public override void Move()
    {
        _animator.enabled = false;
        Tween moveTween = new JumpTween(transform, transform.position, _currentTile.Next.TileMovePoint.position, _jumpTime, _moveTime, Easing.Linear, OnMoveComplete);
    }
}
