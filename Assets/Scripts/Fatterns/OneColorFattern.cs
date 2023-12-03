using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneColorFattern : Fattern
{
    Coroutine touchCheckCoroutine;
    protected override void Start()
    {
        base.Start();
        fatternTimer = 60f / StageDB.stageData[GameManager.Instance.currentStage].bpm;
        answers = new List<int>(2);
        rightAnswers = new List<int>(2) { 0, 0 };
    }
    public override void StartFattern()
    {
        base.StartFattern();
        AnswerChange();
        ColorChange();
    }
    public override void StartTutorialFattern()
    {
        base.StartTutorialFattern();
        answers.Clear();
        rightAnswers[0] = 0;
        rightAnswers[1] = 7;
        ColorChange();
    }
    public override void ExitFattern()
    {
        StopTouchcheck();
        base.ExitFattern();
    }
    public override void ColorChange()
    {
        colorDB.Shuffle();
        Color oneColor = colorDB.FlatColorList(0);
        float diff = 25.5f / 255.0f;
        Color collectcolor = new Color(oneColor.r - diff, oneColor.g - diff, oneColor.b - diff);
        for (int i = 0; i < flats.flat.Length; i++)
        {
            if (IsRightAnswer(i))
            {
                flats.flat[i].ColorChange(collectcolor);
            }
            else
            {
                flats.flat[i].ColorChange(oneColor);
            }
        }
    }

    public override void AnswerChange()
    {
        answers.Clear();
        shuffle();
        for (int i = 0; i < rightAnswers.Count; i++)
        {
            rightAnswers[i] = randomIndex[i];
        }
    }
    public override void InputAnswer(int _num)
    {
        bool check = true;
        for (int i = 0; i < answers.Count; i++)
        {
            if (_num == answers[i])
            {
                check = false;
                break;
            }
        }
        if (check) // 중복체크 통과시만 정답에 추가
        {
            if (touchCheckCoroutine == null)
            {
                touchCheckCoroutine = StartCoroutine(OverTouchTimeCoroutine());
            }
            answers.Add(_num);
            SortAnswer();
        }
    }
    void SortAnswer()
    {
        if (answers.Count == rightAnswers.Count)
        {
            answers.Sort();
            rightAnswers.Sort();
            AnswerCheck();
        }
    }
    void AnswerCheck()
    {
        bool check = true;
        for (int i = 0; i < rightAnswers.Count; i++)
        {
            if (answers[i] != rightAnswers[i]) // 정답과 플레이어가 고른 답이 다를경우
            {
                check = false;
                break;
            }
        }
        if (!check)
        {
            StopTouchcheck();
            flats.ChangeAllColor(colorDB.MissColor);
            SetDecision(Decision.MISS);
        }
        else // 정답인 경우
        {
            TutorialManager.isFatternClear = true;
            PlayAllFlatEffect();
            if (StageManager.fatternTimer / fatternTimer <= 0.25f)
            {
                SetDecision(Decision.PERPECT);
                flats.ChangeAllColor(colorDB.PerfectColor);
            }
            else if (StageManager.fatternTimer / fatternTimer <= 0.5f)
            {
                SetDecision(Decision.GOOD);
                flats.ChangeAllColor(colorDB.GoodColor);
            }
            else
            {
                SetDecision(Decision.EARLY);
                flats.ChangeAllColor(colorDB.MissColor);
            }
            StopTouchcheck();
        }
    }
    IEnumerator OverTouchTimeCoroutine()
    {
        yield return YieldCache.WaitForSeconds(0.2f);
        GameManager.Instance.TouchLock();
        flats.ChangeAllColor(colorDB.MissColor);
        SetDecision(Decision.MISS);
    }
    void StopTouchcheck()
    {
        if (touchCheckCoroutine != null)
        {
            StopCoroutine(touchCheckCoroutine);
            touchCheckCoroutine = null;
        }
    }
    protected override void SetDecision(Decision _decision)
    {
        base.SetDecision(_decision);
    }
    protected override void SetDecisionScore(Decision _decision)
    {
        switch (_decision)
        {
            case Decision.PERPECT:
                scoreManager.IncreaseScore(4);
                break;
            case Decision.GOOD:
                scoreManager.IncreaseScore(2);
                break;
            case Decision.EARLY:
                scoreManager.IncreaseScore(0);
                break;
            case Decision.MISS:
                scoreManager.IncreaseScore(-3);
                break;
        }
    }
}
