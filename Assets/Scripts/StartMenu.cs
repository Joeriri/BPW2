using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    private Intro intro;

    private void Awake()
    {
        intro = GetComponentInParent<Intro>();
    }

    public void StartGame()
    {
        intro.StartIntroPan();
        Debug.Log("Game started.");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit.");
    }
}
