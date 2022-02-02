using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    private SkinManager _skinManager; 
    [SerializeField] private TMP_Text _coinTotal;

    private void Awake()
    {
        _skinManager = FindObjectOfType<SkinManager>();
    }

    private void Start()
    {
        _coinTotal.text = PlayerPrefs.GetInt("Coins").ToString();
        _skinManager.onUpdateCoinTotal += onUpdateCoinTotal;
    }

    private void OnDestroy()
    {
        _skinManager.onUpdateCoinTotal -= onUpdateCoinTotal;

    }

    private void onUpdateCoinTotal()
    {
        _coinTotal.text = PlayerPrefs.GetInt("Coins").ToString();
    }    
}
