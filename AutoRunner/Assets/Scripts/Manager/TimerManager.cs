using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    private Goal _goal;
    private SpawnManager _spawnManager; 

    private bool _isPlaying;
    public bool Playing { set => _isPlaying = value; }
    public Text TimerText;
    private float _currentTime;

    // Update is called once per frame

    private void Awake()
    {
        _goal = FindObjectOfType<Goal>();
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Start()
    {
        _isPlaying = true;
        _goal.OnReachedGoal += OnReachedGoal;
        _spawnManager.OnRespawn += OnRespawn;
    }

    private void OnRespawn()
    {
        _currentTime = 0;
        _isPlaying = true;
    }

    private void OnReachedGoal()
    {
        _isPlaying = false; 
    }

    void Update()
    {
        if (_isPlaying)
        {
            _currentTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(_currentTime / 60f);
            int seconds = Mathf.FloorToInt(_currentTime % 60f);
            int milliseconds = Mathf.FloorToInt((_currentTime * 100f) % 100f);
            TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        }
    }


}
