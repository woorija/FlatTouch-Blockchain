using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exitbutton : MonoBehaviour
{
    public void ExitGame()
    {
        StartCoroutine(CustomSceneManager.Instance.ExitApp());
    }
}
