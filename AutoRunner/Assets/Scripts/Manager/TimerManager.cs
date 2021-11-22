using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    private bool _isPlaying;
    private Goal _goal;
    public bool Playing { set => _isPlaying = value; }
    public Text TimerText;
    private float currentTime;
    // Update is called once per frame

    private void Awake()
    {
        _goal = FindObjectOfType<Goal>();
    }

    private void Start()
    {
        _isPlaying = true;
        _goal.OnReachedGoal += OnReachedGoal;
    }

    private void OnReachedGoal()
    {
        _isPlaying = false; 
    }

    void Update()
    {
        if (_isPlaying)
        {
            currentTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            int milliseconds = Mathf.FloorToInt((currentTime * 100f) % 100f);
            TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        }
    }


}
