using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fattern : MonoBehaviour
{
    protected List<int> answers;
    protected List<int> rightAnswers;
    [SerializeField] protected int fatternIndex;

    protected int[] randomIndex = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }; // 플랫 갯수만큼 있는 정답 플랫 랜덤을 위한 배열
    [SerializeField] protected FlatColorDB colorDB;
    [SerializeField] protected Flats flats;

    protected ScoreManager scoreManager;
    protected EffectManager effectManager;
    protected DecisionManager decisionManager;

    protected float fatternTimer;
    protected virtual void Start()
    {
    }
    public void GetManagers()
    {
        scoreManager = GetComponentInChildren<ScoreManager>();
        effectManager= GetComponentInChildren<EffectManager>();
        decisionManager= GetComponentInChildren<DecisionManager>();
    }
    protected void shuffle()
    {
        for (int index = 0; index < randomIndex.Length; index++)
        {
            int tempIndex = Random.Range(index, randomIndex.Length);
            int temp = randomIndex[index];
            randomIndex[index] = randomIndex[tempIndex];
            randomIndex[tempIndex] = temp;
        }
    }
    protected virtual bool IsRightAnswer(int _num)
    {
        bool _answer = false;
        for (int i = 0; i < rightAnswers.Count; i++)
        {
            if (_num == rightAnswers[i])
            {
                _answer = true;
                break;
            }
        }
        return _answer;
    }
    public virtual void StartFattern() 
    { 
        decisionManager.CurrentDecisionInit();
    }
    public virtual void StartTutorialFattern()
    {
        decisionManager.CurrentDecisionInit();
    }
    public virtual void ExitFattern()
    {
        if (decisionManager.currentDecision == Decision.NONE)
        {
            SetDecision(Decision.MISS);
        }
    }
    public virtual void ColorChange() { }
    public virtual void AnswerChange() { }
    public virtual void InputAnswer(int _num) { }
    protected virtual void SetDecision(Decision _decision)
    {
        GameManager.Instance.TouchLock(); // 판정 이후 리셋때까지 터치금지
        decisionManager.DecisionUpdate(_decision);
        SetDecisionScore(_decision);
        switch (_decision)
        {
            case Decision.PERPECT:
                effectManager.PlayPerfectEffect();
                break;
            case Decision.GOOD:
                effectManager.PlayGoodEffect();
                break;
            case Decision.EARLY:
            case Decision.LATE:
                effectManager.PlayMissEffect();
                break;
            case Decision.MISS:
                StageManager.missCount++;
                if(StageManager.missCount >= 10)
                {
                    StageManager stageManager = FindObjectOfType<StageManager>();
                    stageManager.GameOver();
                }
                if (StageManager.missCount >= 3)
                {
                    effectManager.PlayWarningEffect();
                }
                else
                {
                    effectManager.PlayMissEffect();
                }
                break;
        }
    }
    protected virtual void SetDecisionScore(Decision _decision) { }
    public float GetTimer()
    {
        return fatternTimer;
    }
    public int GetIndex()
    {
        return fatternIndex;
    }
    protected void PlayAllFlatEffect()
    {
        if (GameManager.Instance.isTouchEffectPlay)
        {
            effectManager.SetEndPos();
            for (int i = 0; i < rightAnswers.Count; i++)
            {
                effectManager.flatEffects[rightAnswers[i]].ColorChange(colorDB.FlatColorList(0));
                effectManager.flatEffects[rightAnswers[i]].SetEndpos(effectManager.GetEndPos());
                effectManager.flatEffects[rightAnswers[i]].PlayEffect();
            }
        }
    }
    protected void PlayRGBFlatEffect()
    {
        if (GameManager.Instance.isTouchEffectPlay)
        {
            effectManager.SetEndPos();
            for (int i = 0; i < rightAnswers.Count; i++)
            {
                effectManager.flatEffects[rightAnswers[i]].ColorChange(colorDB.RGBColorList(0));
                effectManager.flatEffects[rightAnswers[i]].SetEndpos(effectManager.GetEndPos());
                effectManager.flatEffects[rightAnswers[i]].PlayEffect();
            }
        }
    }
    protected void PlayFlatEffect(int _index)
    {
        if (GameManager.Instance.isTouchEffectPlay)
        {
            effectManager.SetEndPos();
            effectManager.flatEffects[rightAnswers[_index]].ColorChange(colorDB.RainbowColorList[_index]);
            effectManager.flatEffects[rightAnswers[_index]].SetEndpos(effectManager.GetEndPos());
            effectManager.flatEffects[rightAnswers[_index]].PlayEffect();
        }
    }
}
