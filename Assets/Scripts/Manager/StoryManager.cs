using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    protected int currentStory;
    protected Coroutine check;
    [SerializeField] protected GameObject[] playableModeObjects;
    [SerializeField] protected GameObject pauseUI;

    [SerializeField] protected BGChanger bgChanger;
    [SerializeField] protected DialogueManager dialogueManager;
    
    protected void Awake()
    {
        currentStory = GameManager.Instance.currentStage;
    }
    protected virtual void Start() 
    {
        check = null;
        BGChange();
    }
    protected virtual void BGChange()
    {
        bgChanger.BGChange(currentStory + 1);
        switch (currentStory)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                bgChanger.ChangeGrayScale(0);
                break;
            case 6:
                bgChanger.ChangeGrayScale(30);
                break;
            case 7:
                bgChanger.ChangeGrayScale(100);
                break;
        }
    }
    public void NpcTouch()
    {
        if (check == null)
        {
            check= StartCoroutine(CustomSceneManager.Instance.FadeEvent(NpcTouchCoroutine()));
        }
    }
    protected IEnumerator NpcTouchCoroutine()
    {
        foreach(GameObject go in playableModeObjects)
        {
            go.SetActive(false);
        }
        dialogueManager.StartDialogue();
        yield break;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameManager.Instance.TouchLock();
        AudioManager.Instance.PauseBgm();
        pauseUI.SetActive(true);
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        GameManager.Instance.TouchUnlock();
        AudioManager.Instance.UnpauseBgm();
        pauseUI.SetActive(false);
    }
    public void SceneMoveMenu()
    {
        AudioManager.Instance.UnpauseBgm();
        CustomSceneManager.Instance.LoadScene("03_MenuScene");
    }
}
