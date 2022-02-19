using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private void Start()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState State)
    {
        if(State == GameState.Playable)
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
