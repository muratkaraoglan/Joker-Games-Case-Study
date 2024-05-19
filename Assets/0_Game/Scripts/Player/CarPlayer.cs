using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : PlayerBase
{
    public override void Idle()
    {

    }

    public override void Move()
    {
        Tween moveTween = new MoveTween(transform, transform.position, _currentTile.Next.TileMovePoint.position, _moveTime, Easing.Linear, OnMoveComplete);
        transform.forward = (_currentTile.Next.TileMovePoint.position - transform.position).normalized;
    }
}
