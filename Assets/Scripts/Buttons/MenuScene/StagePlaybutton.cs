using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePlaybutton : MonoBehaviour
{
    public void MoveStageScene()
    {
        CustomSceneManager.Instance.LoadScene("04_StageScene");
    }
}
