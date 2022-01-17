using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [SerializeField] private GameObject _characterSelectPanel;
    [SerializeField] private GameObject _mainMenuPanel;    
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private List<Sprite> skins = new List<Sprite>();
    private int _selectedSkin = 0;
    public GameObject PlayerSkin;
    public GameObject Player;

    private void Start()
    {
        Player.GetComponent<SpriteRenderer>().sprite = _sr.sprite;

    }

    public void TogglePanel()
    {
        if(_characterSelectPanel.activeSelf)
        {
            _characterSelectPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }
        else if(_mainMenuPanel.activeSelf)
        {
            _mainMenuPanel.SetActive(false);
            _characterSelectPanel.SetActive(true);
        }
    }

    public void NextOption()
    {
        _selectedSkin = _selectedSkin + 1; 
        if(_selectedSkin == skins.Count)
        {
            _selectedSkin = 0;
        }

        _sr.sprite = skins[_selectedSkin];

        UpdateSprite();
    }

    public void PreviousOption()
    {
        _selectedSkin = _selectedSkin - 1;
        if (_selectedSkin < 0)
        {
            _selectedSkin = skins.Count - 1;
        }

        _sr.sprite = skins[_selectedSkin];
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        Player.GetComponent<SpriteRenderer>().sprite = _sr.sprite;
    }
}
