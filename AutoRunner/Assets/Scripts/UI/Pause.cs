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
        GameManager.Instance.OnReplayLevel = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Reset();
    }

    public void PreviousLevel()
    {
        GameManager.Instance.OnReplayLevel = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Reset();

    }

    public void ReplayLevel()
    {
        GameManager.Instance.OnReplayLevel = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Reset();

    }

    public void GoBackToMain()
    {
        Reset();
        SceneManager.LoadScene(0);
    }

    private void Reset()
    {
        Time.timeScale = 1;
    }
}
