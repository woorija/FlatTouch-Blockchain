using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseRainbowFattern : Fattern
{
    [SerializeField] DecisionObject decisionObject;
    int count;
    protected override void Start()
    {
        base.Start();
        fatternTimer = 120f / StageDB.stageData[GameManager.Instance.currentStage].bpm;
        answers = new List<int>(7);
        rightAnswers = new List<int>(7) { 0, 1, 2, 3, 4, 5, 6 };
    }

    public override void StartFattern()
    {
        count = 6;
        base.StartFattern();
        AnswerChange();
        ColorChange();
    }
    public override void StartTutorialFattern()
    {
        count = 6;
        base.StartTutorialFattern();
        answers.Clear();
        randomIndex[0] = rightAnswers[0] = 1;
        randomIndex[1] = rightAnswers[1] = 5;
        randomIndex[2] = rightAnswers[2] = 7;
        randomIndex[3] = rightAnswers[3] = 8;
        randomIndex[4] = rightAnswers[4] = 4;
        randomIndex[5] = rightAnswers[5] = 0;
        randomIndex[6] = rightAnswers[6] = 2;
        randomIndex[7] = 6;
        randomIndex[8] = 3;
        ColorChange();
    }
    public override void ExitFattern()
    {
        decisionObject.ColorChange(Color.white);
        base.ExitFattern();
    }
    public override void ColorChange()
    {
        decisionObject.ColorChange(colorDB.RainbowColorList[count]);
        for (int i = 0; i < flats.flat.Length; i++)
        {
            flats.flat[randomIndex[i]].ColorChange(colorDB.RainbowColorList[i]);
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
            if (answers.Count >= 0)
            {
                answers.Add(_num);
                AnswerCheck();
                count--;
            }
        }
    }
    void AnswerCheck()
    {
        bool check = true;
        if (answers[6 - count] != rightAnswers[count])
        {
            check= false;
        }
        if (!check)
        {
            flats.ChangeAllColor(colorDB.MissColor);
            SetDecision(Decision.MISS);
        }
        else // 정답인 경우
        {
            PlayFlatEffect(count);
            if (count == 0)
            {
                TutorialManager.isFatternClear = true;
                if (StageManager.fatternTimer / fatternTimer >= 0.2f)
                {
                    SetDecision(Decision.PERPECT);
                    flats.ChangeAllColor(colorDB.PerfectColor);
                }
                else if (StageManager.fatternTimer / fatternTimer >= 0.05f)
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
            else
            {
                decisionObject.ColorChange(colorDB.RainbowColorList[count-1]);
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
                scoreManager.IncreaseScore(5);
                break;
            case Decision.GOOD:
                scoreManager.IncreaseScore(3);
                break;
            case Decision.LATE:
                scoreManager.IncreaseScore(1);
                break;
            case Decision.MISS:
                scoreManager.IncreaseScore(-3);
                break;
        }
    }
}
