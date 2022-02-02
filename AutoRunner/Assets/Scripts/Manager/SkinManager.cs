using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public event System.Action onUpdateCoinTotal;

    [SerializeField] private GameObject _characterSelectPanel;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Image Image; 
    [SerializeField] private List<Sprite> skins = new List<Sprite>();

    public AnimatorOverrideController[] _animOverrideList;
    public SkinBluePrint[] skinArrray;
    public GameObject PlayerSkin;
    public Button buyButton;


    private static int _selectedSkin = 0;
    
    private void Start()
    {
        foreach(SkinBluePrint skin in skinArrray)
        {
            if(skin.price == 0)
            {
                skin.isUnlocked = true; 
            }
            else
            {
                skin.isUnlocked = PlayerPrefs.GetInt(skin.name, 0) == 0 ? false : true; 
            }
        }

        _selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);

        if (skinArrray[_selectedSkin].isUnlocked)
        {
            UpdateSprite(_selectedSkin);
        }
        else
        {
            UpdateSprite(0);
        }
        Image.sprite = skins[_selectedSkin];
    }

    private void Update()
    {
        UpdateUI();
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
        _selectedSkin++;
        if (_selectedSkin == skins.Count)
        {
            _selectedSkin = 0;
        }

          Image.sprite = skins[_selectedSkin];
        if (skinArrray[_selectedSkin].isUnlocked)
        {
           _sr.sprite = skins[_selectedSkin];
            UpdateSprite(_selectedSkin);
        }
        PlayerPrefs.SetInt("SelectedSkin", _selectedSkin);
    }

    public void PreviousOption()
    {
        _selectedSkin = _selectedSkin - 1;
        if (_selectedSkin < 0)
        {
            _selectedSkin = skins.Count - 1;
        }

        Image.sprite = skins[_selectedSkin];
        if (skinArrray[_selectedSkin].isUnlocked)
        {
            _sr.sprite = skins[_selectedSkin];
            UpdateSprite(_selectedSkin);
        }        
        PlayerPrefs.SetInt("SelectedSkin", _selectedSkin);

    }

    private void UpdateSprite(int index)
    {
        GameManager.Instance.player.GetComponent<SpriteRenderer>().sprite = _sr.sprite;
        GameManager.Instance.player.GetComponent<Animator>().runtimeAnimatorController = _animOverrideList[index] as RuntimeAnimatorController;
        PlayerSkin.GetComponent<SpriteRenderer>().sprite = _sr.sprite;
        PlayerSkin.GetComponent<Animator>().runtimeAnimatorController = _animOverrideList[index] as RuntimeAnimatorController;
        Debug.Log(PlayerPrefs.GetInt("SelectedSkin"));
    }

    public void UnlockSkin()
    {
        SkinBluePrint s = skinArrray[_selectedSkin];
        PlayerPrefs.SetInt(s.name, 1);
        PlayerPrefs.SetInt("SelectedSkin", _selectedSkin);
        s.isUnlocked = true;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - s.price);
        onUpdateCoinTotal?.Invoke();
        UpdateSprite(_selectedSkin);
    }

    private void UpdateUI()
    {
        SkinBluePrint s = skinArrray[_selectedSkin];
        if(s.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy " + s.price;
            if (s.price < PlayerPrefs.GetInt("Coins", 0))
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
    }

}
