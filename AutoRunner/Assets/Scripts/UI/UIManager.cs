using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void GoToLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
