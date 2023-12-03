using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyFattern : Fattern
{
    protected override void Start()
    {
        base.Start();
        fatternTimer = 3f;
    }
    public override void StartFattern()
    {
        scoreManager.SetText("3");
        ColorChange();
        StartCoroutine(Count());
    }
    public override void ExitFattern()
    {
        scoreManager.SetText("0");
    }

    public override void ColorChange()
    {
        for (int i = 0; i < flats.flat.Length; i++)
        {
            flats.flat[i].ColorChange(colorDB.StartColor);
            effectManager.flatEffects[i].PlayEffect();
        }
    }
    void ColorChange(Color _color)
    {
        for (int i = 0; i < flats.flat.Length; i++)
        {
            flats.flat[i].ColorChange(_color);
        }
    }
    public override void AnswerChange()
    {

    }
    public override void InputAnswer(int _num)
    {
    }
    IEnumerator Count()
    {
        yield return YieldCache.WaitForSeconds(1.0f);
        scoreManager.SetText("2");
        ColorChange(colorDB.GoodColor);
        yield return YieldCache.WaitForSeconds(1.0f);
        scoreManager.SetText("1");
        ColorChange(colorDB.PerfectColor);
    }
}
