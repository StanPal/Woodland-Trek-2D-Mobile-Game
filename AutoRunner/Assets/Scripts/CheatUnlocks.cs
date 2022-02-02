using UnityEngine;

public class CheatUnlocks : MonoBehaviour
{
    public void GiveCoins()
    {
        PlayerPrefs.SetInt("Coins", 500);
    }
}
