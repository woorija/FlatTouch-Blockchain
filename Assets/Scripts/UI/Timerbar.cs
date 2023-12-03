using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timerbar : MonoBehaviour
{
    [SerializeField] Image TimerbarR;
    [SerializeField] Image TimerbarL;
    public void TimerbarUpdate(float _aspect)
    {
        TimerbarL.fillAmount= _aspect;
        TimerbarR.fillAmount= _aspect;
    }
}
