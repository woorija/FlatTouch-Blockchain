using System.Collections;
using Unity.Collections;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    [SerializeField] protected FatternManager fatternManager;

    [SerializeField] protected Timerbar timerbar;
    [SerializeField] protected BGChanger BGChanger;

    [SerializeField] GameObject PauseUI;
    [SerializeField] protected GameObject ClearUI;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] GameObject GameoverUI;
    public static float fatternTimer { get; protected set; }
    public static int missCount = 0;
    Coroutine clearCheckCoroutine;
    protected virtual void Start()
    {
        StageInit();
    }
    public virtual void StageInit()
    {
        Time.timeScale = 1f;
        PauseUI.SetActive(false);
        ClearUI.SetActive(false);
        GameoverUI.SetActive(false);
        fatternTimer = 3f;
        missCount = 0;
        fatternManager.FatternInit();
        fatternManager.StartFattern();
        BGSetting();
        if (clearCheckCoroutine != null)
        {
            StopCoroutine(clearCheckCoroutine);
        }
        clearCheckCoroutine = StartCoroutine(ClearCheckCoroutine());

    }
    protected virtual void Update()
    {
        Timecheck();
    }
    protected virtual void Timecheck()
    {
        fatternTimer -= Time.deltaTime;
        timerbar.TimerbarUpdate(1 - (fatternTimer / fatternManager.GetTimer()));
        if (fatternTimer <= 0f)
        {
            fatternManager.ChangeFattern();
            fatternTimer += fatternManager.GetTimer();
        }
    }
    protected virtual float LateBGMTime() //싱크 맞추기
    {
        float time = 0;
        switch (GameManager.Instance.currentStage)
        {
            case 1:
                time = -0.25f;
                break;
            case 2:
                time = -0.1f;
                break;
            case 3:
                break;
            case 4:
                time = -0.37f;
                break;
            case 5:
                time = -0.2f;
                break;
            case 6:
                time = -0.16f;
                break;
            case 7:
                time = -0.06f;
                break;

        }
        return time;
    }
    protected virtual void BGMSetting()
    {
        AudioManager.Instance.StopBgm();
        AudioManager.Instance.PlayBgm($"Stage{GameManager.Instance.currentStage}BGM");
    }
    protected virtual void BGSetting()
    {
        BGChanger.StageBGChange(GameManager.Instance.currentStage);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameManager.Instance.TouchLock();
        AudioManager.Instance.PauseBgm();
        PauseUI.SetActive(true);
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        GameManager.Instance.TouchUnlock();
        AudioManager.Instance.UnpauseBgm();
        PauseUI.SetActive(false);
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        GameManager.Instance.TouchLock();
        GameManager.Instance.GetScore(FindObjectOfType<ScoreManager>().GetScore());
        AudioManager.Instance.StopBgm();
        GameoverUI.SetActive(true);
    }
    public void SceneMoveMenu()
    {
        CustomSceneManager.Instance.LoadScene("03_MenuScene");
    }
    IEnumerator ClearCheckCoroutine()
    {
        yield return YieldCache.WaitForSeconds(fatternTimer + LateBGMTime());
        BGMSetting();
        yield return YieldCache.WaitForSeconds(AudioManager.Instance.GetBGMLength());
        GameClear();
    }
    protected virtual void GameClear()
    {
        Time.timeScale = 0f;
        GameManager.Instance.TouchLock();
        AudioManager.Instance.StopBgm();
        ClearUI.SetActive(true);
        int score = FindObjectOfType<ScoreManager>().GetScore();
        GameManager.Instance.GetScore(score);
        ScoreText.text = score.ToString();
    }
}
