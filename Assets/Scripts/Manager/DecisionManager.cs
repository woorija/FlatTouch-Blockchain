using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Decision
{
    NONE,
    PERPECT,
    GOOD,
    EARLY,
    LATE,
    MISS
}
public class DecisionManager : MonoBehaviour
{
    [SerializeField] TMP_Text decisionText;
    // Start is called before the first frame update
    public Decision currentDecision { get; private set; }
    Color decisionTextColor;
    Color[] decisionColor = new Color[4] { Color.blue, Color.green, Color.yellow, Color.red };
    public void DecisionUpdate(Decision _decision)
    {
        currentDecision = _decision;
        StartCoroutine(DecisionUpdateCoroutine(_decision));
    }
    IEnumerator DecisionUpdateCoroutine(Decision _decision)
    {
        TextColorChange(_decision);
        decisionText.text = _decision.ToString();
        decisionText.color = decisionTextColor;
        yield return new WaitForSeconds(0.2f);
        while (decisionTextColor.a > 0)
        {
            decisionTextColor.a -= 0.05f;
            decisionText.color = decisionTextColor;
            yield return null;
        }
    }
    void TextColorChange(Decision _dicision)
    {
        switch (_dicision)
        {
            case Decision.PERPECT:
                decisionTextColor = decisionColor[0];
                break;
            case Decision.GOOD:
                decisionTextColor = decisionColor[1];
                break;
            case Decision.EARLY:
            case Decision.LATE:
                decisionTextColor = decisionColor[2];
                break;
            case Decision.MISS:
                decisionTextColor = decisionColor[3];
                break;
        }
    }
    public void CurrentDecisionInit()
    {
        currentDecision = Decision.NONE;
    }

    public void DecisionInit()
    {
        decisionTextColor.a = 0;
        decisionText.color = decisionTextColor;
    }
}
