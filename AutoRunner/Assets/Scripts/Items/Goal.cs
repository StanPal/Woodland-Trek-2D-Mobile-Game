using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public event System.Action OnReachedGoal;
    private SpawnManager _spawnManager;
    private CinemachineVirtualCamera _cmv;


    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _cmv = FindObjectOfType<CinemachineVirtualCamera>();

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState State)
    {
        if(State == GameState.LevelStart)
        {
            StartCoroutine(PanToSpawn());
        }
    }

    private IEnumerator PanToSpawn()
    {     

        while (Vector2.Distance(_cmv.LookAt.transform.position, _spawnManager.RespawnPoint.position) > 0.1f)
        {
            _cmv.LookAt.transform.position = Vector2.MoveTowards(_cmv.LookAt.transform.position, _spawnManager.RespawnPoint.position, 1.0f * Time.deltaTime);
            _cmv.Follow.transform.position = Vector2.MoveTowards(_cmv.Follow.transform.position, _spawnManager.RespawnPoint.position, 1.0f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCollision>())
        {
            OnReachedGoal.Invoke();
        }
    }


}
