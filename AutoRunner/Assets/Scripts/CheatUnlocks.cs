using UnityEngine;

public class CheatUnlocks : MonoBehaviour
{
    private CoinManager _coinManager;

    private void Start()
    {
        _coinManager = FindObjectOfType<CoinManager>();
    }

    public void GiveCoins()
    {
        PlayerPrefs.SetInt("Coins", 500);
        _coinManager.onUpdateCoinTotal();
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Tutorial " + 1, 0);
        _coinManager.onUpdateCoinTotal();
    }
}
