using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int score;
    [SerializeField] TMP_Text text;
    BGChanger bgchanger;
    private void Awake()
    {
        bgchanger=FindObjectOfType<BGChanger>();
    }
    public void ScoreInit()
    {
        score = 0;
        bgchanger.ChangeGrayScale(score);
    }
    void SetScore()
    {
        bgchanger.ChangeGrayScale(score);
        text.text = score.ToString();
    }
    public void SetText(string _s)
    {
        text.text = _s;
    }
    public void IncreaseScore(int _score)
    {
        score += _score;
        SetScore();
    }
    public int GetScore()
    {
        return score;
    }
}
