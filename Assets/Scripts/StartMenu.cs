using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Intro intro;

    public void StartGame()
    {
        intro.StartIntro();
        Debug.Log("Game started.");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit.");
    }
}
