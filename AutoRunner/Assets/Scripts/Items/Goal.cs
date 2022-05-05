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
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCollision>())
        {
            OnReachedGoal.Invoke();
        }
    }


}
