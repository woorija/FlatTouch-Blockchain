using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmoteAnimate : MonoBehaviour
{
    [SerializeField] Image image;

    Sprite[] emoteSprites; // P, G, M
    RectTransform emoteRectTransform; // 중심맞추기 용도
    Vector3 emotePosition;
    Vector2 emoteSize;
    float stopPosY;
    float StopPosY_emote2;
    float posX;

    Color emoteAlpha;

    Coroutine AnimationCoroutine;
    Coroutine AlphaCoroutine;


    private void Awake()
    {
        emoteSprites = new Sprite[3];
        emoteRectTransform = image.GetComponent<RectTransform>();
    }
    public void EmoteInit(int _stage)
    {
        emoteSprites[0] = ResourceManager.GetSpriteToAtlas("Emote", "Player_Emote6");
        emoteSprites[1] = ResourceManager.GetSpriteToAtlas("Emote", "Player_Emote4");
        switch (_stage)
        {
            case 1:
                emoteSprites[2] = ResourceManager.GetSpriteToAtlas("Emote", "Player_Emote4");
                stopPosY = 0f;
                StopPosY_emote2 = -200f;
                emoteSize = new Vector2(350f, 705f);
                break;
            case 2:
                emoteSprites[2] = ResourceManager.GetSpriteToAtlas("Emote", "Rabbit_Emote4");
                stopPosY = 0f;
                StopPosY_emote2 = -200f;
                emoteSize = new Vector2(328f, 755f);
                break;
            case 3:
                emoteSprites[2] = ResourceManager.GetSpriteToAtlas("Emote", "Raccoon_Emote1");
                stopPosY = -48f;
                StopPosY_emote2 = -200f;
                emoteSize = new Vector2(410f, 550f);
                break;
            case 4:
                emoteSprites[2] = ResourceManager.GetSpriteToAtlas("Emote", "King_Emote1");
                stopPosY = -16f;
                StopPosY_emote2 = -200f;
                emoteSize = new Vector2(560f, 700f);
                break;
            case 5:
                emoteSprites[2] = ResourceManager.GetSpriteToAtlas("Emote", "Witch_Emote1");
                posX = -48f;
                StopPosY_emote2 = -200f;
                emoteSize = new Vector2(500f, 760f);
                break;
            case 6:
                emoteSprites[2] = ResourceManager.GetSpriteToAtlas("Emote", "Witch_Emote8");
                posX = -48f;
                StopPosY_emote2 = -200f;
                emoteSize = new Vector2(500f, 760f);
                break;
            case 7:
                emoteSprites[2] = ResourceManager.GetSpriteToAtlas("Emote", "Witch_Emote4");
                posX = -8f;
                StopPosY_emote2 = -200f;
                emoteSize = new Vector2(460f, 770f);
                break;
        }
    }

    public void EmoteSetting(int _num)
    {
        image.sprite = emoteSprites[_num];
        switch (_num)
        {
            case 0:
            case 1:
                emotePosition = new Vector3(0f, -600f, 0f);
                emoteRectTransform.sizeDelta = new Vector2(350f, 705f);
                stopPosY = -200f;
                break;
            case 2:
                emotePosition = new Vector3(posX, -600f, 0f);
                emoteRectTransform.sizeDelta = emoteSize;
                stopPosY = StopPosY_emote2;
                break;
        }
        emoteAlpha = Color.white;
        emoteRectTransform.anchoredPosition= emotePosition;
        image.color = emoteAlpha;
    }

    public void StartEmoteAnimation()
    {
        if (AlphaCoroutine != null)
        {
            StopCoroutine(AlphaCoroutine);
        }
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }
        AnimationCoroutine = StartCoroutine(EmoteAnimateCoroutine());
        AlphaCoroutine = StartCoroutine(EmoteAlphaAnimateCoroutine());
    }

    IEnumerator EmoteAnimateCoroutine()
    {
        while (emotePosition.y < stopPosY)
        {
            emotePosition.y *= 0.7f;
            emoteRectTransform.anchoredPosition = emotePosition;
            yield return null;
        }
        emotePosition.y = stopPosY;
        emoteRectTransform.anchoredPosition = emotePosition;
    }

    IEnumerator EmoteAlphaAnimateCoroutine()
    {
        yield return YieldCache.WaitForSeconds(0.5f);
        while (emoteAlpha.a > 0)
        {
            emoteAlpha.a -= 0.04f;
            image.color = emoteAlpha;
            yield return null;
        }
    }
}
