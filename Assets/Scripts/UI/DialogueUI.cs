using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text characternameText;

    StringBuilder dialogueBuilder;
    string originText;

    Coroutine typingCoroutine;
    private void Awake()
    {
        dialogueBuilder = new StringBuilder();
    }
    public void GetName(string _name)
    {
        characternameText.text = _name;
    }
    public void GetDialogueText(string _origin)
    {
        originText = _origin;
    }
    public void SetDialogueText()
    {
        dialogueText.text = dialogueBuilder.ToString();
    }
    void SetOriginDialogue()
    {
        dialogueText.text = originText;
    }
    public void PlayDialogue()
    {
        typingCoroutine = StartCoroutine(TypingDialogueCoroutine(originText));
    }
    IEnumerator TypingDialogueCoroutine(string _dialogue)
    {
        int index = 0;
        while (_dialogue.Length != index)
        {
            dialogueBuilder.Append(_dialogue[index++]);
            SetDialogueText();
            yield return new WaitForSeconds(0.07f);
        }
        dialogueBuilder.Clear();
        DialogueManager.b_IsTypingEnd = true;
        TutorialDialoguePlayer.b_IsTypingEnd = true;
    }
    public void DialogueSkip()
    {
        StopCoroutine(typingCoroutine);
        dialogueBuilder.Clear();
        SetOriginDialogue();
        DialogueManager.b_IsTypingEnd = true;
        TutorialDialoguePlayer.b_IsTypingEnd = true;
    }
}
