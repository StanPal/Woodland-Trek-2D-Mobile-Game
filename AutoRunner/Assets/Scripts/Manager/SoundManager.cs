using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;
    [SerializeField] List<AudioClip> _clipList;
    static bool _onContiniousPlay; 

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (GameManager.Instance.State == GameState.GameStart)
        {
            _musicSource.Stop();
            _musicSource.PlayOneShot(_clipList[0]);
        }
        else if (GameManager.Instance.State == GameState.LevelStart)
        {
            if (SceneManager.GetActiveScene().buildIndex <= 10)
            {
                if (_onContiniousPlay)
                {
                    _musicSource.Stop();
                    _musicSource.PlayOneShot(_clipList[1]);
                    _onContiniousPlay = false;
                }
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffects()
    {
        _effectsSource.mute = !_effectsSource.mute;
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
}

public enum SoundLevelState
{
    Mainmenu, 
    Level1_10,
    Level11_20,
    EndScreen
}
