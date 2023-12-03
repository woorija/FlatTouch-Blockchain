using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    [SerializeField] Slider VolumeSlider;
    private void Awake()
    {
        SetVolumeController();
    }
    void SetVolumeController()
    {
        VolumeSlider.value = GameManager.Instance.GetVolume();
        VolumeSlider.onValueChanged.AddListener(delegate { VolumeControl(); });
    }
    void VolumeControl()
    {
        GameManager.Instance.SetVolume(VolumeSlider.value);
    }
}
