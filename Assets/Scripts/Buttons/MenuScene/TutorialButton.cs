using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    public void MoveTutorialScene()
    {
        CustomSceneManager.Instance.LoadScene("07_TutorialScene");
    }
}
