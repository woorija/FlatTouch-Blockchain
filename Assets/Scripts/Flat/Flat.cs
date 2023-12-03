using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Flat : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] int flatNumber;
    Image flatImage;
    RectTransform flatSize;
    Animation anim;
    FatternManager fatternManager;


    Coroutine check;
    public int Get_flatnumber()
    {
        return flatNumber;
    }
    public void SetFatternManager(FatternManager _fatternManager)
    {
        fatternManager = _fatternManager;
    }
    private void Awake()
    {
        flatImage = transform.GetChild(0).GetComponent<Image>();
        flatSize = GetComponent<RectTransform>();
        anim = GetComponent<Animation>();
    }
    public void ColorChange(Color _color)
    {
        flatImage.color = _color;
    }
    public void PlayChangeAnimation()
    {
        anim.Play("Change");
    }

    public void OnPointerDown(PointerEventData eventData) // 패턴 매니저에 더해줄것
    {
        FlatTouch();
    }

    public void FlatTouch()
    {
        if (GameManager.Instance.isTouchable)
        {
            if (check == null)
            {
                check = StartCoroutine(FlatTouchCoroutine());
            }
            fatternManager.InputAnswer(flatNumber);
        }
    }
    IEnumerator FlatTouchCoroutine()
    {
        flatSize.localScale = Vector3.one;
        while (flatSize.localScale.x < 1.3f)
        {
            flatSize.localScale *= 1.05f;
            yield return null;
        }
        while (flatSize.localScale.x > 1.0f)
        {
            flatSize.localScale *= 0.95f;
            yield return null;
        }
        flatSize.localScale = Vector3.one;
        check = null;
    }
}
