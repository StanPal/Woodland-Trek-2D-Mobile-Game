using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinTotal;

    private void Start()
    {
        _coinTotal.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
