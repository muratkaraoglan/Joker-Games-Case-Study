using System;
using UnityEngine;

public class MoveTween : Tween
{

    public MoveTween(Transform target, Vector3 startPosition, Vector3 endPosition, float duration, EaseFunction ease, Action onComplete = null) : base(target, startPosition, endPosition, duration, ease, onComplete)
    {
        TweenManager.AddTween(this);
    }

    public override void Update()
    {
        if (!IsPlaying) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / Duration);
        t = Ease(t);
        Target.position = Vector3.Lerp(StartPosition, EndPosition, t);

        if (elapsedTime >= Duration)
        {
            IsPlaying = false;
            OnComplete?.Invoke();
            TweenManager.RemoveTween(this);
            Target.position = EndPosition;
        }
    }
}