using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelHolder;
    public GameObject levelIcon;
    public GameObject thisCanvas; 
    [SerializeField] private int _numberOfLevels;
    private Rect panelDimensions;
    private Rect iconDimensions;
    private int amountPerPage;
    private int _currentLevelCount;
    [SerializeField] private Vector2 _iconSpacing; 

    void Start()
    {
        _numberOfLevels = SceneManager.sceneCountInBuildSettings - 3;
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt((panelDimensions.width + _iconSpacing.x) / (iconDimensions.width + _iconSpacing.x));
        int maxInACol = Mathf.FloorToInt((panelDimensions.height + _iconSpacing.y) / (iconDimensions.height + _iconSpacing.y));
        amountPerPage = maxInARow * maxInACol;
        int totalPages = Mathf.CeilToInt((float)_numberOfLevels / amountPerPage);
        LoadPanels(totalPages); 
    }

    private void LoadPanels(int numberofPanels)
    {
        GameObject panelClone = Instantiate(levelHolder) as GameObject; 

        for(int i = 1; i <= numberofPanels; i++)
        {
            GameObject panel = Instantiate(panelClone) as GameObject;
            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            panel.name = "Page-" + i;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i - 1), 0);
            SetUpGrid(panel);
            int numberOfIcons = i == numberofPanels ? _numberOfLevels - _currentLevelCount : amountPerPage; 
            LoadIcons(numberOfIcons, panel);
        }
        Destroy(panelClone);
    }

    void LoadIcons(int numberOfIcons, GameObject parentObject)
    {
        for(int i = 1; i <= numberOfIcons; i++)
        {
            _currentLevelCount++;
            GameObject icon = Instantiate(levelIcon) as GameObject;
            icon.GetComponent<ButtonSelectHandler>().LevelIndex = _currentLevelCount;
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Level " + i;
            icon.GetComponentsInChildren<TextMeshProUGUI>()[0].SetText("Level" + _currentLevelCount);
            if (PlayerPrefs.GetFloat("Level " + i) != 0)
            {
                float time = PlayerPrefs.GetFloat("Level " + i);
                icon.GetComponentsInChildren<TextMeshProUGUI>()[1].SetText(System.Math.Round(time,2).ToString());
                ColorBlock LevelClearedColor = icon.GetComponent<Button>().colors;
                LevelClearedColor.normalColor = Color.yellow;
                LevelClearedColor.highlightedColor = Color.yellow;
                LevelClearedColor.selectedColor = Color.yellow;
                LevelClearedColor.pressedColor = Color.green;
                icon.GetComponent<Button>().colors = LevelClearedColor;
            }
            else
            {
                icon.GetComponent<Button>().interactable = false;
            }
            //icon.GetComponentInChildren<TextMeshProUGUI>().SetText("Level " + _currentLevelCount);
        }
    }
    
    private void SetUpGrid(GameObject panel)
    {
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.spacing = _iconSpacing;
        
    }
    
}
