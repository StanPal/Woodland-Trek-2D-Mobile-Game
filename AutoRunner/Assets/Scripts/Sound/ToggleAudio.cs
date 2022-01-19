using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private Image _soundImage;
    [SerializeField] private List<Sprite> _soundIconList = new List<Sprite>();
    [SerializeField] private bool _toggleMusic, _toggleEffects;
    private int _index = 0;

    private void Start()
    {
        _soundImage = this.GetComponent<Image>();
    }

    public void Toggle()
    {
        if(_toggleEffects)
        {
            SoundManager.Instance.ToggleEffects();
        }
        if(_toggleMusic)
        {
            _soundImage.overrideSprite = _soundIconList[0];
            _index++;
            if (_index > _soundIconList.Count - 1)
            {
                _index = 0;
            }
            SoundManager.Instance.ToggleMusic();
        }
    }


}
