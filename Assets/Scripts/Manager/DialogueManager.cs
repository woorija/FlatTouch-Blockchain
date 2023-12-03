using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject[] dialogueUIs; //다이얼로그 오브젝트 전체

    [SerializeField] PortraitUI portraitUI;
    [SerializeField] DialogueUI dialogueUI;

    public static bool b_IsTypingEnd; // 타 스크립트에서 클릭 제어를 하기 위한 변수
    
    int index;
    DialogueData currentData;
    public void StartDialogue()
    {
        index = GameManager.Instance.currentStage * 1000 + 1;
        foreach (GameObject go in dialogueUIs) {
             go.SetActive(true);
        }
        SetDialogueData();
    }
    void SetDialogueData()
    {
        b_IsTypingEnd = false;
        currentData = DialogueDB.dialogueData[index];
        portraitUI.GetPortrait(currentData.characterNumber);
        if (currentData.characterNumber != 6) // 마법봉 이펙트가 아닐경우
        {
            portraitUI.GetEmote(currentData.emoteNumber);
            dialogueUI.GetName(DialogueDB.characterNames[currentData.characterNumber]);
            dialogueUI.GetDialogueText(currentData.dialogue);
        }

        SetDialogue();
    }
    void SetDialogue()
    {
        portraitUI.PortraitSetting();
        if (currentData.characterNumber != 6) // 마법봉 이펙트가 아닐경우
        {
            dialogueUI.PlayDialogue();
        }
    }
    public void PlayingDialogue()
    {
        if (GameManager.Instance.isTouchable)
        {
            if (b_IsTypingEnd)
            {
                index++;
                if (DialogueDB.dialogueData.ContainsKey(index))
                {
                    SetDialogueData();
                }
                else
                {
                    if (GameManager.Instance.currentStage > GameManager.Instance.storyCleared) //미클리어 스토리시 저장
                    {
                        GameManager.Instance.StoryClear();
                        GameManager.Instance.SaveGame();
                    }
                    CustomSceneManager.Instance.LoadScene("03_MenuScene");
                }
            }
            else
            {
                dialogueUI.DialogueSkip();
            }
        }
    }
}
