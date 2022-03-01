using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TimerManager : MonoBehaviour
{
    public event System.Action OnTimerStopped;
    public event System.Action<float> OnGoalReached;

    private SpawnManager _spawnManager;
    public GameObject _winPanel;
    public GameObject _controllerCanvas; 
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private TMP_Text BestTime;
    [SerializeField] private TMP_Text CountDownText;
    private Goal _goal;
    private bool _isNewBest; 
    private bool _isPlaying;
    private float _currentTime;

    public bool Playing { set => _isPlaying = value; }

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        _goal = FindObjectOfType<Goal>();
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
      if(state == GameState.LevelRestart)
        {
            CountDownText.enabled = false;
            StartCoroutine(RestartDelay());
        }
      else if (state == GameState.LevelStart)
        {            
            StartCoroutine(StartCountDown());
        }
    }

    private void Start()
    {  
        _isNewBest = false;
        _goal.OnReachedGoal += OnReachedGoal;
        _spawnManager.OnRespawn += OnRespawn;
        OnTimerStopped += ToggleWinPanel;
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

    private IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.State = GameState.Playable;
        _isPlaying = true;
    }

    private IEnumerator StartCountDown()
    {
        CountDownText.text = "3";
        yield return new WaitForSeconds(0.7f);
        CountDownText.text = "2";
        yield return new WaitForSeconds(0.7f);
        CountDownText.text = "1";
        yield return new WaitForSeconds(0.7f);
        CountDownText.text = "GO!";
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.State = GameState.Playable;
        CountDownText.enabled = false;
        _isPlaying = true;
    }

    private void ToggleWinPanel()
    {
        if (_winPanel.activeSelf == false)
        {
            Time.timeScale = 0;
            _winPanel.SetActive(true);
            _controllerCanvas.SetActive(false);
        }
        else
        {
            _winPanel.SetActive(false);
        }
    }

    private void OnRespawn()
    {
        GameManager.Instance.UpdateGameState(GameState.Respawn);
        //_currentTime = 0;
        _isPlaying = true;
        GameManager.Instance.UpdateGameState(GameState.Playable);
    }

    private void OnReachedGoal()
    {
        GameManager.Instance.UpdateGameState(GameState.GoalReached);
        _isPlaying = false;
        OnTimerStopped?.Invoke();

        float previousTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name);

        if (previousTime == 0 || _currentTime < previousTime)
        {
            OnGoalReached?.Invoke(_currentTime);
            _isNewBest = true;
        }
        
        if (_isNewBest)
        {
            int minutes = Mathf.FloorToInt(_currentTime / 60f);
            int seconds = Mathf.FloorToInt(_currentTime % 60f);
            int milliseconds = Mathf.FloorToInt((_currentTime * 100f) % 100f);
            //BestTime.text = "New Record! " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
            BestTime.text = "New Record! ";
        }
        else
        {
            //int minutes = Mathf.FloorToInt(previousTime / 60f);
            //int seconds = Mathf.FloorToInt(previousTime % 60f);
            //int milliseconds = Mathf.FloorToInt((previousTime * 100f) % 100f);
            //BestTime.text = "Best Time: " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        }

    }



}
