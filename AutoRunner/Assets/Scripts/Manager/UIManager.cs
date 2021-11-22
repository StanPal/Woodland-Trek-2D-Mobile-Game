using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private TimerManager _timerManager;
    public GameObject _winPanel; 

    private void Awake()
    {
        _timerManager = FindObjectOfType<TimerManager>();
    }

    private void Start()
    {
        _timerManager.OnTimerStopped += ToggleWinPanel;
    }

    private void ToggleWinPanel()
    {
        if(_winPanel.activeSelf == false)
        {
            Time.timeScale = 0;
            _winPanel.SetActive(true);
        }
        else
        {            
            _winPanel.SetActive(false);
        }
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Reset();
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene("Level 1");
        Reset();
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Reset()
    {
        Time.timeScale = 1;
    }
}
