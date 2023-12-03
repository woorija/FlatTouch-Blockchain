using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MagicUI : MonoBehaviour
{
    [SerializeField]
    Image Magic;
    [SerializeField]
    Image MagicBG;
    RectTransform BG_rectTransform;
    Color MagicAlpha;
    private void Awake()
    {
        BG_rectTransform = MagicBG.rectTransform;
    }
    public IEnumerator MagicDialogue()
    {
        GameManager.Instance.TouchLock();
        Magic.gameObject.SetActive(true);
        MagicBG.gameObject.SetActive(true);
        MagicAlpha = new Color(1, 1, 1, 0);
        while (MagicAlpha.a < 1)
        {
            MagicAlpha.a += 0.05f;
            Magic.color = MagicAlpha;
            MagicBG.color = (MagicAlpha * 0.7f) - new Color(1, 1, 1, 0);
            yield return null;
        }
        float timer = 0f;
        while (timer < 1.5f)
        {
            BG_rectTransform.anchoredPosition += Vector2.right * Random.Range(-4, 5);
            timer += Time.deltaTime;
            yield return null;
        }

        while (MagicAlpha.a >= 0)
        {
            MagicAlpha.a -= 0.05f;
            Magic.color = MagicAlpha;
            MagicBG.color = (MagicAlpha * 0.7f) - new Color(1, 1, 1, 0);
            yield return null;
        }

        Magic.gameObject.SetActive(false);
        MagicBG.gameObject.SetActive(false);
        DialogueManager.b_IsTypingEnd = true;
        GameManager.Instance.TouchUnlock();
    }
}
