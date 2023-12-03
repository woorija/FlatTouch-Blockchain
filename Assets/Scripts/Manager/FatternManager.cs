using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FatternManager : MonoBehaviour
{
    [SerializeField] EffectManager effectManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] FatternUI fatternUI;

    [SerializeField] Fattern[] fatterns;
    [SerializeField] Fattern dummyFattern;
    List<Fattern> usingFatterns;
    List<int> usingFatternsProbablilty;
    [SerializeField] Flats flats;


    Fattern currentFattern;
    Fattern nextFattern;

    public float fatternTimer { get; private set; }
    private void Awake()
    {
        usingFatterns = new List<Fattern>();
        usingFatternsProbablilty = new List<int>();
    }
    public void FatternInit()
    {
        effectManager.EffectInit();
        scoreManager.ScoreInit();
        UseFatternSetting();
        FirstFatternSetting();
    }
    public float GetTimer() {
        fatternTimer = currentFattern.GetTimer();
        return fatternTimer;
    }
    void UseFatternSetting() // 스테이지별 사용하는 패턴 세팅해두기
    {
        bool[] isUseFatterns = StageDB.stageData[GameManager.Instance.currentStage].useFatterns;
        for (int i=0;i< isUseFatterns.Length;i++)
        {
            if (isUseFatterns[i])
            {
                usingFatterns.Add(fatterns[i]);
                usingFatternsProbablilty.Add(StageDB.probabilityData[GameManager.Instance.currentStage].fatternsProbability[i]);
            }
        }
        for(int i = 0; i < usingFatterns.Count; i++)
        {
            usingFatterns[i].GetManagers();
        }
        dummyFattern.GetManagers();
    }
    void FirstFatternSetting() // 첫패턴은 더미패턴
    {
        currentFattern = dummyFattern;
        fatternUI.SetDummyFatternUI();

        SelectNextFattern();
        fatternUI.SetNextFatternUI(nextFattern.GetIndex());
    }
    public void StartFattern()
    {
        currentFattern.StartFattern();
    }
    public void ChangeFattern() // 더미 이후 매 패턴 변경
    {
        currentFattern.ExitFattern(); // 현재 패턴 종료
        StartCoroutine(ChangeAnimationPlayCoroutine());

        currentFattern = nextFattern;
        fatternUI.SetCurrentFatternUI();

        SelectNextFattern();
        fatternUI.SetNextFatternUI(nextFattern.GetIndex());

        currentFattern.StartFattern();
    }

    void SelectNextFattern() // 확률에 의거한 다음 패턴 결정 함수
    {
        int _random = Random.Range(0, 100);
        int probablilty = 0;
        for(int i=0;i<usingFatterns.Count;i++)
        {
            probablilty += usingFatternsProbablilty[i];
            if(_random < probablilty)
            {
                nextFattern = usingFatterns[i];
                break;
            }
        }
    }
    IEnumerator ChangeAnimationPlayCoroutine() // 패턴 변경때마다 전 플랫 애니메이션 플레이
    {
        GameManager.Instance.TouchLock();
        flats.PlayAnimation();
        yield return YieldCache.WaitForSeconds(0.1f);
        GameManager.Instance.TouchUnlock();
    }

    public void InputAnswer(int _num)
    {
        currentFattern.InputAnswer(_num);
    }
    #region tutorial
    public void TutorialFatternInit()
    {
        effectManager.EffectInit();
        scoreManager.ScoreInit();
        UseFatternSetting();
        TutorialFirstFatternSetting();
    }
    void TutorialFirstFatternSetting() // 첫패턴은 더미패턴
    {
        currentFattern = dummyFattern;
        fatternUI.SetDummyFatternUI();

        nextFattern = fatterns[0];
        fatternUI.SetNextFatternUI(nextFattern.GetIndex());
    }
    public void ChangeFattern(int _index)
    {
        currentFattern.ExitFattern(); // 현재 패턴 종료
        flats.PlayAnimation();

        currentFattern = fatterns[_index];
        nextFattern = fatterns[_index];
        fatternUI.SetNextFatternUI(nextFattern.GetIndex());
        fatternUI.SetCurrentFatternUI();
        currentFattern.StartTutorialFattern();
    }


    #endregion
}
