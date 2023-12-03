using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClickEffect : SingletonBehaviour<ClickEffect>
{
    [SerializeField]
    ParticleSystem[] clickeffect;
    int play_index;
    private void Start()
    {
        play_index = 0;
    }

    public void Play_ClickEffect(Vector3 _pos)
    {
        clickeffect[play_index].transform.position = _pos;
        clickeffect[play_index++].Play();
        AudioManager.Instance.PlaySfx("Click");
        if(play_index>= clickeffect.Length)
        {
            play_index= 0;
        }
    }
    void Touch()
    {
    #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Play_ClickEffect(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    #endif
        if (Input.touchCount > 0) // 하나 이상의 터치 입력이 있을 경우
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began) // 터치가 시작될 경우
                {
                    Play_ClickEffect(Camera.main.ScreenToWorldPoint(touch.position));
                }
            }
        }
    }
    private void Update()
    {
        Touch();
    }
}
