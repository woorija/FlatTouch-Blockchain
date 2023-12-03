using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordBubble : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Image wordBubble;

    Color speechAlpha;
    Color textAlpha = new Color(1, 1, 1, 0);

    Coroutine speechCoroutine;

    private void OnEnable()
    {
        speechAlpha = textAlpha;
        wordBubble.color = speechAlpha;
        text.color = textAlpha;
    }

    public void WordInit(int _stage)
    {
        switch (_stage)
        {
            case 1:
                text.text = "틀렸어";
                break;
            case 2:
                text.text = "힘내";
                break;
            case 3:
                text.text = "화이팅";
                break;
            case 4:
                text.text = "조금만 더";
                break;
            case 5:
                text.text = "메롱!";
                break;
            case 6:
                text.text = "흥!";
                break;
            case 7:
                text.text = "할수있어";
                break;
        }
    }

    public void SpeechStart()
    {
        if(speechCoroutine != null) { 
            StopCoroutine(speechCoroutine);
        }
        speechCoroutine = StartCoroutine(SpeechStartCoroutine());
    }
    IEnumerator SpeechStartCoroutine()
    {
        yield return YieldCache.WaitForSeconds(0.1f);
        while(speechAlpha.a <= 1f)
        {
            speechAlpha.a += 0.05f;
            wordBubble.color = speechAlpha;
            text.color = speechAlpha - textAlpha;
            yield return null;
        }
        yield return YieldCache.WaitForSeconds(0.3f);
        while (speechAlpha.a > 0f)
        {
            speechAlpha.a -= 0.06f;
            wordBubble.color = speechAlpha;
            text.color = speechAlpha - textAlpha;
            yield return null;
        }
    }
}
