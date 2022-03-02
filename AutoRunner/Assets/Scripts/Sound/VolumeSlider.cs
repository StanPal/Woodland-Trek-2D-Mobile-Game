using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private static float _sliderNum;

    private void Start()
    {
        _sliderNum = SoundManager.Instance.UpdateMasterVolume();
        SoundManager.Instance.ChangeMasterVolume(_sliderNum);
        _slider.value = _sliderNum;
        _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
    }
}
