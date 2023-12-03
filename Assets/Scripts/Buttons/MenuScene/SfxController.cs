using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{
    public void EffectOn()
    {
        GameManager.Instance.EffectOn();
    }
    public void EffectOff()
    {
        GameManager.Instance.EffectOff();
    }
}
