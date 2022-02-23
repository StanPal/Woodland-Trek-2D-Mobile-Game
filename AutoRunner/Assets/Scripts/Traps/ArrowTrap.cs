using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    private SpawnManager _spawnManager;

    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject[] _arrows;
    private float _coolDownTimer;
    public float ArrowSpeed { get => _speed; }

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Start()
    {
        _spawnManager.OnRespawn += OnRespawn;
    }

    private void OnRespawn()
    {
        for(int i = 0; i < _arrows.Length; i++)
        {
            _arrows[i].SetActive(false);
        }
    }

    private void Fire()
    {
        _coolDownTimer = 0;
        _arrows[FindArrow()].transform.position = _firePoint.position;
        _arrows[FindArrow()].GetComponent<Projectile>().ActivateProjectile();
    }

    private int FindArrow()
    {
        for(int i = 0; i < _arrows.Length; i++)
        {
            if(!_arrows[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }

    private void Update()
    {
        _coolDownTimer += Time.deltaTime;

        if(_coolDownTimer >= _attackCooldown)
        {
            Fire();
        }
    }
}
