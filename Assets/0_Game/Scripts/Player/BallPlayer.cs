using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPlayer : PlayerBase
{
    public override void Idle()
    {
        print("Idle");
    }

    public override void Move()
    {
        Tween moveTween = new JumpTween(transform, transform.position, _currentTile.Next.TileMovePoint.position, .5f, 1f, Easing.Linear, OnMoveComplete);
    }
}
