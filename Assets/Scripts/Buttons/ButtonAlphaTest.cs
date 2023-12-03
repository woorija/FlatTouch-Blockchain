using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAlphaTest : MonoBehaviour
{
    Image image;
    void Start()
    {
        image=GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 0.5f; // 알파0.5이상만 클릭가능
    }
}
