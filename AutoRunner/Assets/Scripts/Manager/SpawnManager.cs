using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform Respawn;
    private PlayerCollision _playerCollision;

    private void Awake()
    {
        _playerCollision = FindObjectOfType<PlayerCollision>();
    }

    void Start()
    {
        _playerCollision.OnDeath += _playerCollision_OnDeath;
    }

    private void _playerCollision_OnDeath(GameObject obj)
    {
        obj.transform.position = Respawn.transform.position;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
