using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPlaybutton : MonoBehaviour
{
    public void MoveStoryScene()
    {
        CustomSceneManager.Instance.LoadScene("05_StoryScene");
    }
}
