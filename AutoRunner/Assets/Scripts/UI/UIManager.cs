using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingsCanvas;
    [SerializeField] private GameObject MainMenuCanvas;

    public void SettingsMenuOn()
    {
        MainMenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
    }

    public void SettingsMenuOff()
    {
        SettingsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

}
