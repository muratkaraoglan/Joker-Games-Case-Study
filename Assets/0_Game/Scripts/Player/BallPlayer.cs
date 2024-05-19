using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPlayer : PlayerBase
{
    [SerializeField] private Animator _animator;
    public override void Idle()
    {
        _animator.enabled = true;
    }

    public override void Move()
    {
        _animator.enabled = false;
        Tween moveTween = new JumpTween(transform, transform.position, _currentTile.Next.TileMovePoint.position, .5f, 1f, Easing.Linear, OnMoveComplete);
    }
}
