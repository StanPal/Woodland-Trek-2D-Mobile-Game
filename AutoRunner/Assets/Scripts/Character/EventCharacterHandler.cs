using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCharacterHandler : MonoBehaviour
{
    private PlayerController _playerController;
    private SpawnManager _spawnManager;
    private GameObject player;

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Start()
    {
        //Setting a variable for this reference doesn't work apparently. 
        //_playerController = _spawnManager.Player.GetComponent<PlayerController>();
    }

    public void OnClick()
    {
        _spawnManager.Player.GetComponent<PlayerController>().Jump();
    }

    public void OnRelease()
    {
        _spawnManager.Player.GetComponent<PlayerController>().ReleaseHoldJumpPress();

    }

    public void OnHold()
    {
        _spawnManager.Player.GetComponent<PlayerController>().HoldJumpPress();
    }

}
