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
    [SerializeField] List<AudioClip> _sfxList;
    [SerializeField] List<AudioClip> _characterSound; 
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

        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }


    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (GameManager.Instance.State == GameState.GameStart)
        {
            PlayClip(0);
        }
        else if (GameManager.Instance.State == GameState.LevelStart)
        {
            if (SceneManager.GetActiveScene().buildIndex == 12)
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
        else
        {
            return; 
        }
    }

    private void PlayClip(int clipIndex)
    {           
        _musicSource.Stop();
        _musicSource.clip = _clipList[clipIndex];
        _musicSource.Play();        
    }

    public void PlaySound(int sfxIndex)
    {
        _effectsSource.PlayOneShot(_sfxList[sfxIndex]);
    }

    public void PlayCharacterEffect(int CharSfxIndex)
    {
        _effectsSource.PlayOneShot(_characterSound[CharSfxIndex]);
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
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

