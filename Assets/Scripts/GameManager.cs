using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Intro intro;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // Singleton
        instance = this;

        // Refs
        intro = FindObjectOfType<Intro>();
    }

    private void Start()
    {
        intro.StartTitleScreen();
    }
}
