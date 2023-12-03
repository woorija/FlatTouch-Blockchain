using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialoguePlayer : MonoBehaviour
{
    [SerializeField] GameObject dialogueBG;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] RectTransform dialogueUIRectTransform;
    [SerializeField] FadeObjectController objectController;

    Vector2 DialoguePos = new Vector2(0, 500);
    public static string[] tutorialInformation =
    {
        "본격적으로 색을 되찾기 전에\n색을 되찾는 방법을 배워봅시다!",
        "스테이지를 시작하면\n 3초의 대기시간이 주어집니다.",
        "좌상단에는 현재 패턴과\n 다음 패턴이 보여집니다.",
        "패턴은 총 5가지가 있습니다.",
        string.Empty,
        "처음으로 알아볼 패턴은\n기본 패턴입니다.",
        "기본패턴은 같은 색상을 가진\n플랫 2개를 찾아\n동시에 터치하는 패턴입니다.",
        "타이밍바가 중앙에 근접할수록\n높은 점수를 얻을 수 있습니다.",
        "그럼 직접 플레이 해보겠습니다.",
        string.Empty,
        "다음으로 알아볼 패턴은\n삼원색 패턴입니다.",
        "삼원색 패턴은 판정구슬과\n같은 색상의 플랫을\n모두 터치하는 패턴입니다.",
        "최대한 빨리 터치할수록\n높은 점수를 얻을 수 있습니다.",
        "그럼 직접 플레이 해보겠습니다.",
        string.Empty,
        "잘하셨습니다.",
        "다음으로 알아볼 패턴은\n무지개 패턴입니다.",
        "무지개 패턴은 무지개 색상\n다시말해 '빨주노초파남보'순서대로\n플랫을 터치하는 패턴입니다.",
        "많은 플랫을 터치해야하기에\n많은 시간이 주어지며\n빨리 터치할수록\n높은 점수를 얻을 수 있습니다.",
        "그럼 직접 플레이 해보겠습니다.",
        string.Empty,
        "얼마 남지 않았습니다.",
        "다음으로 알아볼 패턴은\n단색 패턴입니다.",
        "다른 플랫에 비해 진한 색상을 띄는\n플랫 2개를 찾아\n동시에 터치하는 패턴입니다.",
        "기본 패턴과 마찬가지로\n타이밍바가 중앙에 근접할수록\n높은 점수를 얻을 수 있습니다.",
        "그럼 직접 플레이 해보겠습니다.",
        string.Empty,
        "마지막 입니다.",
        "알아볼 마지막 패턴은\n역 무지개 패턴입니다.",
        "무지개 패턴의 반대순서대로 플랫을 터치하는 패턴입니다.",
        "그럼 직접 플레이 해보겠습니다.",
        string.Empty,
        "잘하셨습니다.",
        "모든 패턴을 통과했으니 색을 되찾을 준비를 마치셨습니다.",
        "부디 모든 색을 되찾기를 기원합니다.",
        string.Empty
    };
    public static bool b_IsTypingEnd; // 타 스크립트에서 클릭 제어를 하기 위한 변수
    int index = -1;
    public void StartDialogue(int _index)
    {
        index = _index;
        dialogueBG.SetActive(true);
        SetDialogueData();
    }
    void SetDialogueData()
    {
        b_IsTypingEnd = false;
        dialogueUI.GetDialogueText(tutorialInformation[index]);
        SetDialoguePos();
        objectController.SetActiveObject(index);
        dialogueUI.PlayDialogue();
    }
    public void PlayingDialogue()
    {
        if (b_IsTypingEnd)
        {
            index++;
            if (!tutorialInformation[index].Equals(string.Empty))
            {
                SetDialogueData();
            }
            else
            {
                TutorialManager.tutorialIndex++;
                TutorialManager.isTutorialPlay = true;
                dialogueBG.SetActive(false);
            }
        }
        else
        {
            dialogueUI.DialogueSkip();
        }
    }

    void SetDialoguePos()
    {
        switch (index)
        {
            case 0:
            case 8:
            case 13:
            case 19:
            case 25:
            case 30:
                dialogueUIRectTransform.anchoredPosition = Vector2.zero;
                break;
            case 6:
            case 11:
            case 17:
            case 23:
            case 29:
                dialogueUIRectTransform.anchoredPosition = DialoguePos;
                break;
            default:
                break;
        }
    }
}
