using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    private PlayerCollision _playerCollision;
    private SpawnManager _spawnManager;
    private Animator _animator; 
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _playerCollision = GetComponent<PlayerCollision>();

        _spawnManager.OnRespawn += OnRespawn;
        _playerCollision.OnCoinPickup += CoinCollected;
    }

    private void OnDestroy()
    {
        _spawnManager.OnRespawn -= OnRespawn;
        _playerCollision.OnCoinPickup -= CoinCollected;
    }

    private void OnRespawn()
    {
        _animator.SetBool("IsDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CoinCollected()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1 );
        Debug.Log(PlayerPrefs.GetInt("Coins"));
    }
}
