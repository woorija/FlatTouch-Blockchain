using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : StageManager
{
    [SerializeField] TutorialDialoguePlayer DialoguePlayer;

    public static int tutorialIndex;
    public static bool isTutorialPlay;
    int fatternIndex = 0;
    public static bool isFatternClear = false;
    private void Awake()
    {
        GameManager.Instance.ChangeStage(0);
    }
    protected override void Start()
    {
        StageInit();
    }
    protected override void Update()
    {
        IndexCheck();
        switch (tutorialIndex)
        {
            case 2:
            case 4:
            case 6:
            case 8:
            case 10:
            case 12:
                base.Update();
                break;
        }
    }
    void IndexCheck()
    {
        if (isTutorialPlay)
        {
            isTutorialPlay = false;
            switch (tutorialIndex)
            {
                case 0:
                    TutorialStart();
                    GameManager.Instance.TouchLock();
                    break;
                case 1:
                    DialoguePlayer.StartDialogue(0);
                    break;
                case 2:
                    Time.timeScale = 1f;
                    fatternManager.StartFattern();
                    break;
                case 3:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(5);
                    break;
                case 4:
                case 6:
                case 8:
                case 10:
                case 12:
                    GameManager.Instance.TouchUnlock();
                    isFatternClear = false;
                    break;
                case 5:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(10);
                    break;
                case 7:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(15);
                    break;
                case 9:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(21);
                    break;
                case 11:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(27);
                    break;
                case 13:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(32);
                    break;
                case 14:
                    TutorialEnd();
                    break;
            }
        }
    }
    protected override void Timecheck()
    {
        fatternTimer -= Time.deltaTime;
        timerbar.TimerbarUpdate(1 - (fatternTimer / fatternManager.GetTimer()));
        if (fatternTimer <= 0f)
        {
            missCount = 0;
            if (tutorialIndex == 2)
            {
                tutorialIndex++;
                fatternManager.ChangeFattern(0);
                fatternTimer += fatternManager.GetTimer();
                isTutorialPlay = true;
            }
            else {
                if (isFatternClear)
                {
                    isFatternClear = false;
                    tutorialIndex++;
                    fatternManager.ChangeFattern(Mathf.Min(++fatternIndex, 4));
                    fatternTimer += fatternManager.GetTimer();
                    isTutorialPlay = true;
                }
                else
                {
                    tutorialIndex--;
                    fatternManager.ChangeFattern(fatternIndex);
                    fatternTimer += fatternManager.GetTimer();
                    isTutorialPlay = true;
                }
            }
        }
    }

    public override void StageInit()
    {
        fatternTimer = 3f;
        missCount = 0;
        fatternManager.TutorialFatternInit();
        BGSetting();
        BGMSetting();
        tutorialIndex = 0;
        isTutorialPlay = true;
    }
    public void TutorialStart()
    {
        isTutorialPlay = true;
        tutorialIndex = 1;
    }
    protected override void BGSetting()
    {
        BGChanger.StageBGChange(1);
    }
    protected override void BGMSetting()
    {
        AudioManager.Instance.StopMainBGM();
        AudioManager.Instance.PlayMainBGM();
    }
    void TutorialEnd()
    {
        Time.timeScale = 0f;
        ClearUI.SetActive(true);
    }
    public void SceneMoveTitle()
    {
        PlayerPrefs.SetInt("FirstPlay", 1);
        CustomSceneManager.Instance.LoadScene("02_TitleScene");
    }
}
