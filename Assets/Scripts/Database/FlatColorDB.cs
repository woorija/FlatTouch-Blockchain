using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Colordata
{
    public int type;
    public Color color;
}

public class FlatColorDB : MonoBehaviour
{
    public Color PerfectColor { get; private set; } = Color.blue;
    public Color GoodColor { get; private set; } = Color.green;
    public Color MissColor { get; private set; } = new Color(139 / 255f, 0, 1);
    public Color StartColor { get; private set; } = new Color(0, 0, 0, 0);

    public Colordata[] ColorList; // 인스펙터로 컬러 생성

    public Color[] RainbowColorList; // 무지개패턴 전용 컬러 리스트
    int[] ColorIndex; //일반 컬러 셔플용
    int[] RGBColorIndex;//RGB 셔플용
    private void Awake()
    {
        ColorIndex = new int[ColorList.Length];
        for(int i = 0; i < ColorIndex.Length; i++)
        {
            ColorIndex[i] = i;
        }
        RGBColorIndex= new int[3];
        RGBColorIndex[0] = 0;
        RGBColorIndex[1] = 3;
        RGBColorIndex[2] = 4;
    }
    public void Shuffle()
    {
        for (int index = 0; index < ColorIndex.Length; index++)
        {
            int randomIndex = Random.Range(index, ColorIndex.Length);
            int temp = ColorIndex[index];
            ColorIndex[index] = ColorIndex[randomIndex];
            ColorIndex[randomIndex] = temp;
        }
    }
    public void RGBShuffle()
    {
        for (int index = 0; index < RGBColorIndex.Length; index++)
        {
            int randomIndex = Random.Range(index, RGBColorIndex.Length);
            int temp = RGBColorIndex[index];
            RGBColorIndex[index] = RGBColorIndex[randomIndex];
            RGBColorIndex[randomIndex] = temp;
        }
    }
    
    public Color FlatColorList(int _index)
    {
        return ColorList[ColorIndex[_index]].color;
    }
    public Color RGBColorList(int _index) // RGB 3색만 쓰는 RGB패턴을 위한 캡슐화
    {
        return RainbowColorList[RGBColorIndex[_index]];
    }
}
