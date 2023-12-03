using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBFattern : Fattern
{
    [SerializeField] DecisionObject decisionObject;
    protected override void Start()
    {
        base.Start();
        fatternTimer = 60f / StageDB.stageData[GameManager.Instance.currentStage].bpm;
        answers = new List<int>(3);
        rightAnswers = new List<int>(3) { 0, 0, 0 };
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
        rightAnswers[0] = 2;
        rightAnswers[1] = 3;
        rightAnswers[2] = 7;
        ColorChange();
    }
    public override void ExitFattern()
    {
        decisionObject.ColorChange(Color.white);
        base.ExitFattern();
    }
    public override void ColorChange()
    {
        colorDB.RGBShuffle();
        int count = 0;
        int colorcount = 1;
        decisionObject.ColorChange(colorDB.RGBColorList(0));
        for (int i = 0; i < flats.flat.Length; i++)
        {
            if (IsRightAnswer(i))
            {
                flats.flat[i].ColorChange(colorDB.RGBColorList(0));
            }
            else
            {
                flats.flat[i].ColorChange(colorDB.RGBColorList(colorcount));
                count++;
                colorcount = (count / 3) + 1;
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
            flats.ChangeAllColor(colorDB.MissColor);
            SetDecision(Decision.MISS);
        }
        else // 정답인 경우
        {
            TutorialManager.isFatternClear = true;
            PlayRGBFlatEffect();
            if (StageManager.fatternTimer / fatternTimer >= 0.4f)
            {
                SetDecision(Decision.PERPECT);
                flats.ChangeAllColor(colorDB.PerfectColor);
            }
            else if (StageManager.fatternTimer / fatternTimer >= 0.1f)
            {
                SetDecision(Decision.GOOD);
                flats.ChangeAllColor(colorDB.GoodColor);
            }
            else
            {
                SetDecision(Decision.LATE);
                flats.ChangeAllColor(colorDB.MissColor);
            }
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
                scoreManager.IncreaseScore(3);
                break;
            case Decision.GOOD:
                scoreManager.IncreaseScore(2);
                break;
            case Decision.LATE:
                scoreManager.IncreaseScore(0);
                break;
            case Decision.MISS:
                scoreManager.IncreaseScore(-3);
                break;
        }
    }
}
