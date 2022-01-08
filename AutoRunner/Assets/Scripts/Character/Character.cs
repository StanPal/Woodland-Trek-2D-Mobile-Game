using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    private PlayerCollision _playerCollision;


    void Start()
    {
        _playerCollision = GetComponent<PlayerCollision>();
        _playerCollision.OnCoinPickup += CoinCollected;
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
