using Cinemachine;
using UnityEngine;

public class Character : MonoBehaviour
{
    private PlayerCollision _playerCollision;
    private SpawnManager _spawnManager;
    private Animator _animator; 
    private CinemachineVirtualCamera _cmv;

    private void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _cmv = FindObjectOfType<CinemachineVirtualCamera>();

        _animator = GetComponent<Animator>();
        _playerCollision = GetComponent<PlayerCollision>();

        _spawnManager.OnRespawn += OnRespawn;
        _playerCollision.OnCoinPickup += CoinCollected;


        _cmv.LookAt = this.transform;
        _cmv.Follow = this.transform;
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

    private void CoinCollected()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1 );
        Debug.Log(PlayerPrefs.GetInt("Coins"));
    }
}
