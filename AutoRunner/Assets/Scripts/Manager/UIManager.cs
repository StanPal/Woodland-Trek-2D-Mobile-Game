using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Reset();
    }
   
    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Reset();
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene("Level 1");
        Reset();
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(0);
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
