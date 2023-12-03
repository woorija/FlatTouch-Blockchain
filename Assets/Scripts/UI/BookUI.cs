using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    [SerializeField] GameObject Book;
    [SerializeField] TMP_Text title_text;
    [SerializeField] Button[] BookmarkBG;
    [SerializeField] GameObject[] Bookmark;
    [SerializeField] GameObject[] Silhouette;
    [SerializeField] Button currentPage;
    bool isStage;

    int current_select_number;

    public void BookOpen(bool _isStage)
    {
        isStage= _isStage;
        Book.SetActive(true);
        currentPage.onClick.RemoveAllListeners();
        title_text.text = null;
        BookmarkInit();
    }

    void BookmarkInit()
    {
        for(int i = 0; i < 7; i++)
        {
            int index = i;
            Bookmark[i].gameObject.SetActive(false);
            Silhouette[i].gameObject.SetActive(false);
            if (GetClearInformation() >= index)
            {
                BookmarkBG[index].gameObject.SetActive(true);
            }
            else
            {
                BookmarkBG[index].gameObject.SetActive(false);
            }
        }
    }

    public void Click_Bookmark(int _index)//미선택 북마크 버튼에서 사용
    {
        if (current_select_number >= 1) //이미 선택된 북마크가 있으면
        {
            BookmarkBG[current_select_number - 1].gameObject.SetActive(true);
            Bookmark[current_select_number - 1].gameObject.SetActive(false);
            Silhouette[current_select_number - 1].gameObject.SetActive(false);
        }
        BookmarkBG[_index - 1].gameObject.SetActive(false);
        Bookmark[_index - 1].gameObject.SetActive(true);
        Silhouette[_index - 1].gameObject.SetActive(true);
        current_select_number = _index;
        SelectPage();
    }

    void SelectPage()
    {
        currentPage.onClick.RemoveAllListeners();
        if(isStage)
        {
            title_text.text = "STAGE\n" + current_select_number;
            currentPage.onClick.AddListener(() => CustomSceneManager.Instance.LoadScene("04_StageScene"));
        }
        else
        {
            if(current_select_number == 7)
            {
                title_text.text = "ENDING";
                currentPage.onClick.AddListener(() => CustomSceneManager.Instance.LoadScene("06_EndingScene"));
            }
            else
            {
                title_text.text = "STORY\n" + current_select_number;
                currentPage.onClick.AddListener(() => CustomSceneManager.Instance.LoadScene("05_StoryScene"));
            }
        }
        currentPage.onClick.AddListener(() => GameManager.Instance.ChangeStage(current_select_number));
        currentPage.onClick.AddListener(() => DontDoubleClick());
    }

    void DontDoubleClick()
    {
        currentPage.interactable = false;
    }

    int GetClearInformation()
    {
        if (isStage)
        {
            return GameManager.Instance.storyCleared;
        }
        else
        {
            return GameManager.Instance.stageCleared - 1;
        }
    }
}
