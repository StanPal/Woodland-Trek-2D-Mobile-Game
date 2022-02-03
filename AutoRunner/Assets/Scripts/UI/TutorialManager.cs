using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private int _currentLevel = 0;
    private int _listIndex = 0;
    public List<TMP_Text> TextList = new List<TMP_Text>();
    public static bool _isTutorialOn;

    private void Start()
    {
        if(_isTutorialOn)
        {
            TextList[0].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        //if(Input.touchCount > 0)
        //{
        //    Debug.Log("ScreenTapped");
            
        //}
    }

    public void NextMessage()
    {
        switch (_currentLevel)
        {
            case 1:

                break;
            case 2:
                break;
            default:
                break;
        }

    }

}
