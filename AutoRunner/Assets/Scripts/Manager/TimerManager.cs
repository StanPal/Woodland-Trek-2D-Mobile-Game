using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public event System.Action OnTimerStopped;
    public event System.Action<float> OnGoalReached;
    private SpawnManager _spawnManager;

    public GameObject _winPanel;
    public Text TimerText;
    public Text BestTime;
    private Goal _goal;
    private bool _isNewBest; 
    private bool _isPlaying;
    private float _currentTime;

    public bool Playing { set => _isPlaying = value; }

    private void Awake()
    {
        _goal = FindObjectOfType<Goal>();
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Start()
    {
        _isNewBest = false;
        _isPlaying = true;
        _goal.OnReachedGoal += OnReachedGoal;
        _spawnManager.OnRespawn += OnRespawn;
        OnTimerStopped += ToggleWinPanel;
        Debug.Log(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name));
    }

    private void ToggleWinPanel()
    {
        if (_winPanel.activeSelf == false)
        {
            Time.timeScale = 0;
            _winPanel.SetActive(true);
        }
        else
        {
            _winPanel.SetActive(false);
        }
    }

    private void OnRespawn()
    {
        _currentTime = 0;
        _isPlaying = true;
    }

    private void OnReachedGoal()
    {
        _isPlaying = false;
        OnTimerStopped?.Invoke();

        float previousTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name);

        if (previousTime == 0 || _currentTime < previousTime)
        {
            OnGoalReached?.Invoke(_currentTime);
            _isNewBest = true;
            Debug.Log("BetterTimeReached");
        }
        
        if (_isNewBest)
        {
            int minutes = Mathf.FloorToInt(_currentTime / 60f);
            int seconds = Mathf.FloorToInt(_currentTime % 60f);
            int milliseconds = Mathf.FloorToInt((_currentTime * 100f) % 100f);
            BestTime.text = "New Record! " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        }
        else
        {
            int minutes = Mathf.FloorToInt(previousTime / 60f);
            int seconds = Mathf.FloorToInt(previousTime % 60f);
            int milliseconds = Mathf.FloorToInt((previousTime * 100f) % 100f);
            BestTime.text = "Best Time: " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        }

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
