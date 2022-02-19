using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonSelectHandler : MonoBehaviour
{
    private int _levelIndex = 1;
    public int LevelIndex { get => _levelIndex; set => _levelIndex = value; }

    public void OnSelectLevel()
    {
        SceneManager.LoadScene("Level " + _levelIndex);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
