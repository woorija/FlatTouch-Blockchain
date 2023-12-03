using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGChanger : MonoBehaviour
{
    [SerializeField] SpriteRenderer BG;
    [SerializeField] SpriteRenderer FG;
    [SerializeField] Material GrayScaleMaterial;
    public void BGChange(int _index)
    {
        switch (_index)
        {
            case 1:
            case 2:
            case 3:
            case 5:
                BG.sprite = ResourceManager.GetSprite($"BGs/BG{_index}");
                FG.sprite = ResourceManager.GetSprite($"BGs/FG{_index}");
                break;
            case 6:
            case 7:
                BG.sprite = ResourceManager.GetSprite($"BGs/BG6");
                FG.sprite = ResourceManager.GetSprite($"BGs/FG6");
                break;
            case 4:
                BG.sprite = ResourceManager.GetSprite($"BGs/BG{_index}");
                FG.sprite = null;
                break;
        }
    }
    public void ChangeGrayScale(int _score)
    {
        float value = Mathf.Clamp(_score / 100f, 0f, 1f);
        GrayScaleMaterial.SetFloat("_Saturation", value);
    }
    public void StageBGChange(int _index)
    {
        switch (_index)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                BG.sprite = ResourceManager.GetSprite($"StageBGs/BG{_index}");
                break;
            case 7:
                BG.sprite = ResourceManager.GetSprite($"StageBGs/BG6");
                break;
        }
    }
}
