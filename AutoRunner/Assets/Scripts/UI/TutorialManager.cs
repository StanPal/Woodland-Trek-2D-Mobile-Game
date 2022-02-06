using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private int _currentLevel = 0;
    public List<TMP_Text> TextList = new List<TMP_Text>();
    public List<GameObject> ArrowList = new List<GameObject>();
    public GameObject UICanvas;
    public GameObject ControllerCanvas;
    public GameObject Coin; 
    public bool _isTutorialOn;
    private int _listIndex = 0;
    private SpawnManager _spawnManager; 

    private void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _isTutorialOn = PlayerPrefs.GetInt("Tutorial " + _currentLevel.ToString(), 0) == 0 ? true : false;
        if (_isTutorialOn)
        {
            TextList[0].gameObject.SetActive(true);
        }
        else
        {
            StartLevel();
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown && _isTutorialOn)
        {
            NextMessage();
           Debug.Log("ScreenTapped");
        }
    }

    public void ToggleTutorial()
    {
        if(_isTutorialOn)
        {
            _isTutorialOn = false;
            PlayerPrefs.SetInt("Tutorial " + _currentLevel.ToString(), 1);
        }
        else
        {
            _isTutorialOn = true;
            PlayerPrefs.SetInt("Tutorial " + _currentLevel.ToString(), 0);
        }
    }

    public void NextMessage()
    {
        switch (_currentLevel)
        {
            case 1:
                Level1Messages();
                break;
            default:
                break;
        }      
    }

    private void Level1Messages()
    {
        DeactivateLists();
        _listIndex++;
        if (_listIndex < TextList.Count)
        {
            switch (_listIndex)
            {
                case 1:
                    ArrowList[0].SetActive(true);
                    TextList[_listIndex].gameObject.SetActive(true);
                    break;
                case 2:
                    UICanvas.SetActive(true);
                    TextList[_listIndex].gameObject.SetActive(true);
                    TextList[_listIndex + 1].gameObject.SetActive(true);
                    TextList[_listIndex + 2].gameObject.SetActive(true);
                    _listIndex = _listIndex + 2;
                    break;
                case 5:
                    ControllerCanvas.SetActive(true);
                    TextList[_listIndex].gameObject.SetActive(true);
                    TextList[_listIndex + 1].gameObject.SetActive(true);
                    TextList[_listIndex + 2].gameObject.SetActive(true);
                    _listIndex = _listIndex + 2;
                    break;
                case 8:
                    TextList[_listIndex].gameObject.SetActive(true);
                    ArrowList[1].SetActive(true);
                    Coin.SetActive(true);
                    break;
                case 9:
                    TextList[_listIndex].gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        else
        {
            StartLevel();
        }
    }


    private void StartLevel()
    {
        UICanvas.SetActive(true);
        ControllerCanvas.SetActive(true);
        Coin.SetActive(true);
        for (int i = 0; i < TextList.Count; i++)
        {
            TextList[i].gameObject.SetActive(false);
        }
        _listIndex = 0;
        _isTutorialOn = false;
        PlayerPrefs.SetInt("Tutorial " + _currentLevel.ToString(), 1);
        GameManager.Instance.UpdateGameState(GameState.LevelStart);
        _spawnManager.SpawnPlayer();
    }

    private void DeactivateLists()
    {
        UICanvas.SetActive(false);
        ControllerCanvas.SetActive(false);
        Coin.SetActive(false);
        for (int i = 0; i < TextList.Count; i++)
        {
            TextList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < ArrowList.Count; i++)
        {
            ArrowList[i].gameObject.SetActive(false);
        }
    }
}
