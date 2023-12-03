using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

[Serializable]
public class ScreenFaderBehaviour : PlayableBehaviour
{
    public Color color = Color.black;
    public float saturation = 1f;
}
