using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private TimerManager _timerManager;

    private void Awake()
    {
        _timerManager = FindObjectOfType<TimerManager>();
    }
    private void Start()
    {
        _timerManager.OnGoalReached += SaveTime;
    }

    private void SaveTime(string time)
    {
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, time);
    }
}
