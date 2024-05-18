using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate float EaseFunction(float t);

public static class Easing
{
    public static float Linear(float t) => t;

    public static float EaseInQuad(float t) => t * t;
    public static float EaseOutQuad(float t) => t * (2 - t);
    public static float EaseInOutQuad(float t) => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;

    public static float EaseInCubic(float t) => t * t * t;
    public static float EaseOutCubic(float t) => (--t) * t * t + 1;
    public static float EaseInOutCubic(float t) => t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;

    public static float EaseInQuart(float t) => t * t * t * t;
    public static float EaseOutQuart(float t) => 1 - (--t) * t * t * t;
    public static float EaseInOutQuart(float t) => t < 0.5f ? 8 * t * t * t * t : 1 - 8 * (--t) * t * t * t;

    public static float EaseInQuint(float t) => t * t * t * t * t;
    public static float EaseOutQuint(float t) => 1 + (--t) * t * t * t * t;
    public static float EaseInOutQuint(float t) => t < 0.5f ? 16 * t * t * t * t * t : 1 + 16 * (--t) * t * t * t * t;

    public static float EaseInSine(float t) => 1 - Mathf.Cos(t * Mathf.PI / 2);
    public static float EaseOutSine(float t) => Mathf.Sin(t * Mathf.PI / 2);
    public static float EaseInOutSine(float t) => -(Mathf.Cos(Mathf.PI * t) - 1) / 2;

    public static float EaseInExpo(float t) => t == 0 ? 0 : Mathf.Pow(2, 10 * (t - 1));
    public static float EaseOutExpo(float t) => t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
    public static float EaseInOutExpo(float t) => t == 0 ? 0 : t == 1 ? 1 : t < 0.5 ? Mathf.Pow(2, 20 * t - 10) / 2 : (2 - Mathf.Pow(2, -20 * t + 10)) / 2;

    public static float EaseInCirc(float t) => 1 - Mathf.Sqrt(1 - t * t);
    public static float EaseOutCirc(float t) => Mathf.Sqrt(1 - (--t) * t);
    public static float EaseInOutCirc(float t) => t < 0.5 ? (1 - Mathf.Sqrt(1 - 4 * t * t)) / 2 : (Mathf.Sqrt(1 - 4 * (--t) * t) + 1) / 2;
}