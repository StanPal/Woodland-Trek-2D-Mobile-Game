using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt(panelDimensions.width / iconDimensions.width);
        int maxInACol = Mathf.FloorToInt(panelDimensions.height / iconDimensions.height);
        amountPerPage = maxInARow * maxInACol;
        int totalPages = Mathf.CeilToInt((float)_numberOfLevels / amountPerPage);
        LoadPanels(totalPages); 
    }

    private void LoadPanels(int nubmerofPanels)
    {
        GameObject panelClone = Instantiate(levelHolder) as GameObject; 

        for(int i = 1; i <= _numberOfLevels; i++)
        {
            GameObject panel = Instantiate(panelClone) as GameObject;
            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            panel.name = "Page-" + i;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i - 1), 0);
            SetUpGrid(panel);
            int numberOfIcons = i == amountPerPage ? _numberOfLevels - _currentLevelCount : amountPerPage; 
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
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Level " + i;
        }
    }
    
    private void SetUpGrid(GameObject panel)
    {
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        
    }
    
}
