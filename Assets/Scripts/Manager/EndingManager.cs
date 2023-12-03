using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : StoryManager
{
    int currentBGIndex;
    //[SerializeField] 성 오브젝트
    [SerializeField] GameObject[] NPCs;
    [SerializeField] GameObject trigger;
    [SerializeField] GameObject castle;

    [SerializeField] SpriteRenderer player;
    protected override void Start()
    {
        Init();
        base.Start();
    }
    void Init()
    {
        currentBGIndex = 8;
        bgChanger.ChangeGrayScale(100);
        PlayerSetting();
        BGChange();
    }
    void PlayerSetting()
    {
        if (currentBGIndex == 4)
        {
            PlayerSettingLeft();
        }
        else
        {
            PlayerSettingRight();
        }
    }
    void PlayerSettingRight()
    {
        player.gameObject.transform.position = new Vector3(3200, -900, 0);
        player.flipX = true;
    }
    void PlayerSettingLeft()
    {
        player.gameObject.transform.position = new Vector3(-100, -900, 0);
        player.flipX = false;
    }
    protected override void BGChange()
    {
        currentBGIndex--;
        switch (currentBGIndex)
        {
            case 4: // 성 내부는 성 주변에서 성 클릭으로 이동하기때문에 패스
                currentBGIndex--;
                break;
            case 1:
                trigger.SetActive(false);
                castle.SetActive(true);
                break;
            case 0:
                currentBGIndex = 4;
                castle.SetActive(false);
                EnableNpc();
                break;
        }
        bgChanger.BGChange(currentBGIndex);
    }
    void EnableNpc()
    {
        foreach(GameObject go in NPCs)
        {
            go.SetActive(true);
        }
    }

    public void SceneChange()
    {
        if (check == null)
        {
            check=StartCoroutine(CustomSceneManager.Instance.FadeEvent(SceneChangeCoroutine()));
        }
    }
    IEnumerator SceneChangeCoroutine()
    {
        Time.timeScale = 10;
        BGChange();
        PlayerSetting();
        yield return null;
        check = null;
        yield return null;
        Time.timeScale = 1;
    }
}
