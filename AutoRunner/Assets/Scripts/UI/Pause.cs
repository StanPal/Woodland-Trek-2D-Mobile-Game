using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Pause : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject ControllerCanvas;

    public void OnPause()
    {
        Time.timeScale = 0;
        ControllerCanvas.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void OnResume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        ControllerCanvas.SetActive(true);
    }

    public void NextLevel()
    {
        GameManager.Instance.UpdateGameState(GameState.LevelStart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PreviousLevel()
    {
        GameManager.Instance.UpdateGameState(GameState.LevelStart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ReplayLevel()
    {
        GameManager.Instance.UpdateGameState(GameState.LevelStart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBackToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
