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
        _timerManager = FindObjectOfType<TimerManager>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            UpdateGameState(GameState.GameStart);
        }
        else
        {
            UpdateGameState(GameState.LevelStart);
        }
    }

    private void SaveTime(float time)
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, time);
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

}

public enum GameState
{
    GameStart,
    LevelStart,
    Respawn,
    GoalReached
}
