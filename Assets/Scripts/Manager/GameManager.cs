using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    float audioVolume = 1f; //볼륨세팅
    public int stageCleared { get; private set; } = 5;
    public int storyCleared { get; private set; } = 4;
    public int score { get; private set; } = 0;
    public int currentStageSroce { get; private set; } = 0;
    public int currentStage { get; private set; } = 0; // 스테이지씬의 리소스 적용을 위한 현스테이지상태 변수
    public bool isTouchable { get; private set; } = false; //터치 가능 상태
    public bool isTouchEffectPlay { get; private set; } = false; //터치이펙트 적용 유무
    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        LoadGame();
    }
    public void SetVolume(float _volume)
    {
        audioVolume = _volume;
        AudioManager.Instance.VolumeControl();
    }
    public void GetScore(int _score)
    {
        if (currentStage > stageCleared)
        {
            if (_score > 0)
            {
                score += _score;
                if (score >= 100)
                {
                    stageCleared++; //스테이지클리어
                    score = 0;
                }
                SaveGame();
            }
        }
    }

    public float GetVolume() { return audioVolume; }
    public void ChangeStage(int _stage)
    {
        currentStage = _stage;
    }
    public void StoryClear()
    {
        storyCleared++;
        SaveGame();
    }
    public void TouchLock()
    {
        isTouchable = false;
    }
    public void TouchUnlock()
    {
        isTouchable = true;
    }
    public void EffectOn()
    {
        isTouchEffectPlay = true;
    }
    public void EffectOff()
    {
        isTouchEffectPlay = false;
    }
    public void SaveGame()
    {
        if (isTouchEffectPlay)
        {
            PlayerPrefs.SetInt("Sfx", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Sfx", 0);
        }
        PlayerPrefs.SetFloat("Volume", audioVolume);
        PlayerPrefs.SetInt("CStage", stageCleared);
        PlayerPrefs.SetInt("CStory", storyCleared);
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    void LoadGame()
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("Sfx", 1) == 1)
        {
            isTouchEffectPlay = true;
        }
        else
        {
            isTouchEffectPlay = false;
        }
        audioVolume = PlayerPrefs.GetFloat("Volume", 0.3f);
        AudioManager.Instance.VolumeControl();
        stageCleared = PlayerPrefs.GetInt("CStage", 5);
        storyCleared = PlayerPrefs.GetInt("CStory", 4);
        score = PlayerPrefs.GetInt("Score", 0);
    }
    public void LoadToBlockChain()
    {
        for (int i = 6; i > 0; i--)
        {
            if (Web3singleton.Instance.hasBadge[i])
            {
                if(i + 1 > stageCleared)
                {
                    stageCleared = i + 1;
                    storyCleared = i;
                    SaveGame();
                    break;
                }
            }
        }
    }
}
