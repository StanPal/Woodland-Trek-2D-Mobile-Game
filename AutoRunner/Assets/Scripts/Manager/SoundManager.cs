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
    private AudioSource _audioSource;
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

    private void Start()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
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
                PlayClip(1);
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
        _audioSource.clip = _clipList[clipIndex];
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

    public void ToggleEffects()
    {
        _effectsSource.mute = !_effectsSource.mute;
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
}

