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
    static float volume;

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

    private void Start()
    {

    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (GameManager.Instance.State == GameState.GameStart)
        {
            PlayClip(0);
        }
        else if (GameManager.Instance.State == GameState.LevelStart)
        {
            if (SceneManager.GetActiveScene().buildIndex > 11)
            {
                if(_musicSource != null && _musicSource != _clipList[1])
                {
                    PlayClip(1);
                }
            }
        }
        else if (GameManager.Instance.State == GameState.GameEnd)
        {
            PlayClip(2);
        }
    }

    private void PlayClip(int clipIndex)
    {           
        _musicSource.Stop();
        _musicSource.clip = _clipList[clipIndex];
        _musicSource.Play();
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

    public float UpdateMasterVolume()
    {
        return AudioListener.volume;
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

