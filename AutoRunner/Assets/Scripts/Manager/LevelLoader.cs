using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    [SerializeField] private float _transitionTime = 1.0f;

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.OnReplayLevel = true; 
        Reset();
    }
   
    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.Instance.OnReplayLevel = false;
    }

    public void GoToLevel()
    {
        StartCoroutine(LoadLevel(1));
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

    private IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("StartTrigger");

        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(levelIndex);
        Reset();

    }
}
