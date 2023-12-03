using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : SingletonBehaviour<CustomSceneManager>
{
    [SerializeField]
    CanvasGroup fadeImage;
    void Start()
    {
        fadeImage.alpha = 0;
        fadeImage.blocksRaycasts = true;
    }
    public void LoadScene(string scenename)
    {
        StartCoroutine(SceneFadeCoroutine(scenename));
    }

    IEnumerator FadeInCoroutine()
    {
        GameManager.Instance.TouchLock();
        fadeImage.blocksRaycasts = true;
        Time.timeScale = 0;
        while (fadeImage.alpha < 1)
        {
            fadeImage.alpha += 0.02f;
            yield return null;
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        while (fadeImage.alpha > 0)
        {
            fadeImage.alpha -= 0.02f;
            yield return null;
        }
        Time.timeScale = 1;
        GameManager.Instance.TouchUnlock();
        fadeImage.blocksRaycasts = false;
    }

    IEnumerator SceneFadeCoroutine(string scenename)
    {
        yield return StartCoroutine(FadeInCoroutine());
        SceneManager.LoadScene(scenename);
        ResourceManager.UnloadAsset();
        yield return new WaitForSecondsRealtime(0.5f);
        if (scenename.Equals("04_StageScene"))
        {
            AudioManager.Instance.StopMainBGM();
        }
        else
        {
            AudioManager.Instance.PlayMainBGM();
        }
        yield return StartCoroutine(FadeOutCoroutine());
    }

    public IEnumerator FadeEvent(IEnumerator _tempCoroutine)
    {
        yield return StartCoroutine(FadeInCoroutine());
        yield return StartCoroutine(_tempCoroutine);
        yield return new WaitForSecondsRealtime(0.5f);
        yield return StartCoroutine(FadeOutCoroutine());
    }

    public IEnumerator ExitApp()
    {
        yield return StartCoroutine(FadeInCoroutine());
        Application.Quit();
    }
}
