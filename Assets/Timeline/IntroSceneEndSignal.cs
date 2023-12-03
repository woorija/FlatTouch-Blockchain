using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneEndSignal : MonoBehaviour
{
    public void SceneMove_Tutorial()
    {
        CustomSceneManager.Instance.LoadScene("07_TutorialScene");
    }
}
