using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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
        
    }

    public void LoadDesertLevel()
    {
        SceneManager.LoadScene("Desert");
    }

    public void LoadTempleLevel()
    {
        SceneManager.LoadScene("Temple");
    }

    public void LoadEnd()
    {
        SceneManager.LoadScene("End");
    }
    
    public void LoadTitleMenu()
    {
        SceneManager.LoadScene("TitleMenu");
    }
}
