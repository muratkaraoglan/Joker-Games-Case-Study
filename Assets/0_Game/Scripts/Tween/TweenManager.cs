using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenManager : Singelton<TweenManager>
{
    private List<Tween> activeTweens = new List<Tween>();

    void Update()
    {
        for (int i = 0; i < activeTweens.Count; i++)
        {
            activeTweens[i].Update();
        }
    }

    public static void AddTween(Tween tween)
    {
        Instance.activeTweens.Add(tween);
        tween.Play();
    }

    public static void RemoveTween(Tween tween)
    {
        Instance.activeTweens.Remove(tween);
        tween = null;
    }
}
