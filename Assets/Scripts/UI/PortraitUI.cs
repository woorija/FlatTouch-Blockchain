using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitUI : MonoBehaviour
{
    [SerializeField] Image leftPortrait;
    [SerializeField] Image rightPortrait;
    RectTransform leftPortraitRectTransform;
    RectTransform rightPortraitRectTransform;

    int portraitNumber;
    int emoteNumber;

    [SerializeField] MagicUI magicUI;

    private void Awake()
    {
        leftPortraitRectTransform = leftPortrait.rectTransform;
        rightPortraitRectTransform = rightPortrait.rectTransform;
    }
    public void GetPortrait(int _num)
    {
        portraitNumber = _num;
    }
    public void GetEmote(int _num)
    {
        emoteNumber = _num;
    }
    public void PortraitSetting()
    {
        switch (portraitNumber)
        {
            case 0:
                leftPortrait.gameObject.transform.SetSiblingIndex(2);
                rightPortraitRectTransform.gameObject.transform.SetSiblingIndex(0);
                EmoteSetting();
                StartCoroutine(LeftPortraitAnimationCoroutine());
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                leftPortrait.gameObject.transform.SetSiblingIndex(0);
                rightPortraitRectTransform.gameObject.transform.SetSiblingIndex(2);
                EmoteSetting();
                StartCoroutine(RightPortraitAnimationCoroutine());
                break;
            case 5:
                leftPortrait.gameObject.transform.SetSiblingIndex(2);
                rightPortraitRectTransform.gameObject.transform.SetSiblingIndex(2);
                EmoteSetting();
                StartCoroutine(LeftPortraitAnimationCoroutine());
                StartCoroutine(RightPortraitAnimationCoroutine());
                break;
            case 6:
                StartCoroutine(magicUI.MagicDialogue());
                break;
        }
    }
    void EmoteSetting()
    {
        switch (portraitNumber)
        {
            case 0: // 주인공
                leftPortraitRectTransform.anchoredPosition = new Vector2(-300, -350);
                leftPortraitRectTransform.sizeDelta = new Vector2(400, 800);
                leftPortrait.sprite = ResourceManager.GetSpriteToAtlas("Emote", $"Player_Emote{emoteNumber + 1}");
                break;
            case 1: // 토끼
                rightPortraitRectTransform.anchoredPosition = new Vector2(270, -350);
                rightPortraitRectTransform.sizeDelta = new Vector2(360, 830);
                rightPortrait.sprite = ResourceManager.GetSpriteToAtlas("Emote", $"Rabbit_Emote{emoteNumber + 1}");
                break;
            case 2: // 너구리
                rightPortraitRectTransform.anchoredPosition = new Vector2(250, -370);
                rightPortraitRectTransform.sizeDelta = new Vector2(440, 610);
                rightPortrait.sprite = ResourceManager.GetSpriteToAtlas("Emote", $"Raccoon_Emote{emoteNumber + 1}");
                break;
            case 3: // 왕
                rightPortraitRectTransform.anchoredPosition = new Vector2(270, -300);
                rightPortraitRectTransform.sizeDelta = new Vector2(670, 860);
                rightPortrait.sprite = ResourceManager.GetSpriteToAtlas("Emote", $"King_Emote{emoteNumber + 1}");
                break;
            case 4: // 마녀
                rightPortraitRectTransform.anchoredPosition = new Vector2(250, -310);
                rightPortraitRectTransform.sizeDelta = new Vector2(500, 770);
                rightPortrait.sprite = ResourceManager.GetSpriteToAtlas("Emote", $"Witch_Emote{emoteNumber + 1}");
                break;
            case 5: //엔딩전용
                leftPortraitRectTransform.anchoredPosition = new Vector2(-280, -350);
                leftPortraitRectTransform.sizeDelta = new Vector2(330, 820);
                rightPortraitRectTransform.anchoredPosition = new Vector2(250, -370);
                rightPortraitRectTransform.sizeDelta = new Vector2(440, 610);
                leftPortrait.sprite = ResourceManager.GetSpriteToAtlas("Emote", "Rabbit_Emote5");
                rightPortrait.sprite = ResourceManager.GetSpriteToAtlas("Emote", "Raccoon_Emote3");
                break;
        }
    }
    IEnumerator LeftPortraitAnimationCoroutine()
    {
        float increasePosY = 5f;
        int count = 0;
        while (count < 5)
        {
            leftPortraitRectTransform.anchoredPosition = new Vector2(leftPortraitRectTransform.anchoredPosition.x, leftPortraitRectTransform.anchoredPosition.y - increasePosY);
            count++;
            yield return YieldCache.WaitForSeconds(0.016f);
        }
        while (count < 10)
        {
            leftPortraitRectTransform.anchoredPosition = new Vector2(leftPortraitRectTransform.anchoredPosition.x, leftPortraitRectTransform.anchoredPosition.y - increasePosY);
            count++;
            yield return YieldCache.WaitForSeconds(0.016f);
        }
    }
    IEnumerator RightPortraitAnimationCoroutine()
    {
        float increasePosY = 5f;
        int count = 0;
        while (count < 5)
        {
            rightPortraitRectTransform.anchoredPosition = new Vector2(rightPortraitRectTransform.anchoredPosition.x, rightPortraitRectTransform.anchoredPosition.y - increasePosY);
            count++;
            yield return YieldCache.WaitForSeconds(0.016f);
        }
        while (count < 10)
        {
            rightPortraitRectTransform.anchoredPosition = new Vector2(rightPortraitRectTransform.anchoredPosition.x, rightPortraitRectTransform.anchoredPosition.y - increasePosY);
            count++;
            yield return YieldCache.WaitForSeconds(0.016f);
        }
    }
}
