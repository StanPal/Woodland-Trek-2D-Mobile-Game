using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public event System.Action OnTimerStopped; 

    private SpawnManager _spawnManager;
    private Goal _goal;
    private bool _isPlaying;
    private float _currentTime;
    public Text TimerText;

    public bool Playing { set => _isPlaying = value; }
    
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
        OnTimerStopped?.Invoke();
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
