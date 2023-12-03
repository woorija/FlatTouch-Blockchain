using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settingbutton : MonoBehaviour
{
    public void StartGame()
    {
        CustomSceneManager.Instance.LoadScene("03_MenuScene");
    }
}
