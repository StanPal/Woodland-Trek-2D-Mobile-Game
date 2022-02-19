using UnityEngine;

public class CheatUnlocks : MonoBehaviour
{
    public void GiveCoins()
    {
        PlayerPrefs.SetInt("Coins", 500);
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Tutorial " + 1, 0);
    }
}
