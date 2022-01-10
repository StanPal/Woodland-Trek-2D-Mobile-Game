using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Pause : MonoBehaviour
{
    public static event Action OnGamePause; 
    public GameObject PausePanel;
    
    public void OnPause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void OnResume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void NextLevel()
    {
        GameManager.Instance.UpdateGameState(GameState.LevelStart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoBackToMain()
    {
        SceneManager.LoadScene(0);
    }
}
