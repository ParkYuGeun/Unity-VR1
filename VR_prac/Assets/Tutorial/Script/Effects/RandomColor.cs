using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomColor : MonoBehaviour
{
    public float huemin = 0f;
    public float huemax = 1f;
    public float saturationMin = 0.7f;
    public float saturationMax = 1f;
    public float valueMin = 0.7f;
    public float valueMax = 1f;

    public UnityEvent<Color> onCreated;

    public void Call()
    {
        var color = Random.ColorHSV(huemin, huemax, saturationMin, saturationMax, valueMin, valueMax);
        onCreated.Invoke(color);
    }
}
