using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance; 
    [SerializeField] private GameObject _characterSelectPanel;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Image Image; 
    [SerializeField] private List<Sprite> skins = new List<Sprite>();

    public AnimatorOverrideController[] _animOverrideList;
    
    private static int _selectedSkin = 0;
    public GameObject PlayerSkin;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.player.GetComponent<SpriteRenderer>().sprite = _sr.sprite;
        GameManager.Instance.player.GetComponent<Animator>().runtimeAnimatorController = _animOverrideList[_selectedSkin] as RuntimeAnimatorController;
        PlayerSkin.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.player.GetComponent<SpriteRenderer>().sprite;
        PlayerSkin.GetComponent<Animator>().runtimeAnimatorController = _animOverrideList[_selectedSkin] as RuntimeAnimatorController;
        Image.sprite = skins[_selectedSkin];
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
        Image.sprite = skins[_selectedSkin];
        UpdateSprite(_selectedSkin);
    }

    public void PreviousOption()
    {
        _selectedSkin = _selectedSkin - 1;
        if (_selectedSkin < 0)
        {
            _selectedSkin = skins.Count - 1;
        }

        _sr.sprite = skins[_selectedSkin];
        Image.sprite = skins[_selectedSkin];
        UpdateSprite(_selectedSkin);
    }

    private void UpdateSprite(int index)
    {
        GameManager.Instance.player.GetComponent<SpriteRenderer>().sprite = _sr.sprite;
        GameManager.Instance.player.GetComponent<Animator>().runtimeAnimatorController = _animOverrideList[index] as RuntimeAnimatorController;
        PlayerSkin.GetComponent<SpriteRenderer>().sprite = _sr.sprite;
        PlayerSkin.GetComponent<Animator>().runtimeAnimatorController = _animOverrideList[index] as RuntimeAnimatorController;
    }
}
