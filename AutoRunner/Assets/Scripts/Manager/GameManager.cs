using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged; 

    private TimerManager _timerManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _timerManager = FindObjectOfType<TimerManager>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            UpdateGameState(GameState.GameStart);
        }
        else
        {
            UpdateGameState(GameState.LevelStart);
        }
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.GameStart:
                break;
            case GameState.LevelStart:
                break;
            case GameState.Respawn:
                break;
            case GameState.GoalReached:
                _timerManager.OnGoalReached += SaveTime;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void SaveTime(float time)
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, time);
    }

}

public enum GameState
{
    GameStart,
    LevelStart,
    Respawn,
    GoalReached
}
