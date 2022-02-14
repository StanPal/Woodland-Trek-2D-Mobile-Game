using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnGameStateChanged; 
    public GameState State;
    public GameObject player;
    static private bool _replayLevel; 
    public bool OnReplayLevel { get => _replayLevel; set => _replayLevel = value; }

    private TimerManager _timerManager;
    private SpawnManager _spawnManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _timerManager = FindObjectOfType<TimerManager>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            UpdateGameState(GameState.GameStart);
        }
        else if( _replayLevel)
        {            
            UpdateGameState(GameState.LevelRestart);
        }
        else if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            UpdateGameState(GameState.GameEnd);
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                UpdateGameState(GameState.Tutorial);
            }
            else
            {
                UpdateGameState(GameState.LevelStart);
            }
        }
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.GameStart:
                break;
            case GameState.Tutorial:
                break; 
            case GameState.LevelStart:
                SpawnManager.Instance.SpawnPlayer();
                Time.timeScale = 1;
                break;
            case GameState.LevelRestart:
                break;
            case GameState.Playable:
                break;
            case GameState.Respawn:
                break;
            case GameState.GoalReached:
                _timerManager.OnGoalReached += SaveTime;
                break;
            case GameState.GameEnd:
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
    Tutorial,
    LevelStart,
    LevelRestart,
    Playable,
    Respawn,
    GoalReached,
    GameEnd
}
