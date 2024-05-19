using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTween : Tween
{
    private float _jumpHeight;
    public JumpTween(Transform target, Vector3 startPosition, Vector3 endPosition, float duration,float jumpHeight, EaseFunction ease, Action onComplete = null) : base(target, startPosition, endPosition, duration, ease, onComplete)
    {
        _jumpHeight = jumpHeight;
        TweenManager.AddTween(this);
    }

    public override void Update()
    {
        if (!IsPlaying) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / Duration);
        t = Ease(t);

        Vector3 horizontalPosition = Vector3.Lerp(StartPosition, EndPosition, t);
        float parabolicT = t * 2 - 1; 
        float verticalOffset = -parabolicT * parabolicT + 1; 
        Vector3 finalPosition = new Vector3(horizontalPosition.x, horizontalPosition.y + verticalOffset * _jumpHeight, horizontalPosition.z);

        Target.position = finalPosition;

        if (elapsedTime >= Duration)
        {
            IsPlaying = false;
            OnComplete?.Invoke();
            TweenManager.RemoveTween(this);
        }
    }
}
