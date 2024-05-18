using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tween
{

    public Transform Target { get; private set; }
    public Vector3 StartPosition { get; private set; }
    public Vector3 EndPosition { get; private set; }
    public float Duration { get; private set; }
    public Action OnComplete { get; private set; }
    public bool IsPlaying { get; set; }
    public EaseFunction Ease { get; private set; }

    protected float elapsedTime;

    protected Tween(Transform target, Vector3 startPosition, Vector3 endPosition, float duration, EaseFunction ease, Action onComplete = null)
    {
        Target = target;
        StartPosition = startPosition;
        EndPosition = endPosition;
        Duration = duration;
        Ease = ease;
        OnComplete = onComplete;
    }

    public void Play()
    {
        IsPlaying = true;
        elapsedTime = 0f;
    }

    public abstract void Update();
}
