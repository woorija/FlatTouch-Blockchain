using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatternUI : MonoBehaviour
{
    [SerializeField] Sprite[] fatternSprites;
    [SerializeField] Sprite dummyFatternSprite;

    [SerializeField] Image currentFatternImage;
    [SerializeField] Image nextFatternImage;
    public void SetDummyFatternUI()
    {
        currentFatternImage.sprite = dummyFatternSprite;
    }
    public void SetNextFatternUI(int _index)
    {
        nextFatternImage.sprite = fatternSprites[_index];
    }
    public void SetCurrentFatternUI()
    {
        currentFatternImage.sprite = nextFatternImage.sprite;
    }
}
