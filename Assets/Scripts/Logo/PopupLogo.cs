using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupLogo : MonoBehaviour
{
    private void Start()
    {
        //PlayerPrefs.SetInt("FirstPlay", 0);
        PlayerPrefs.DeleteKey("Address");
        StartCoroutine(ScenemoveCoroutine());
    }


    public void SceneMove()
    {
        if(PlayerPrefs.GetInt("FirstPlay", 0) == 0)
        {
            //CustomSceneManager.Instance.LoadScene("01_IntroScene");
            CustomSceneManager.Instance.LoadScene("02_TitleScene");
            //CustomSceneManager.Instance.LoadScene("07_TutorialScene");
        }
        else
        {
            CustomSceneManager.Instance.LoadScene("02_TitleScene");
            
        }
    }

    IEnumerator ScenemoveCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        SceneMove();
    }
}
