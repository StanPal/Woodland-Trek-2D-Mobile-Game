using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterTracker : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private CinemachineVirtualCamera _cmv; 

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _cmv = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _cmv.Follow = _spawnManager.Player.transform;
    }    

}
