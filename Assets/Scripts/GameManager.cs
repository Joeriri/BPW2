using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Refs
    private LevelUI levelUI;

    // Vars

    // Singleton
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
        levelUI = FindObjectOfType<LevelUI>();
    }

    private void Start()
    {
        levelUI.StartFade(Color.white, new Color(255, 255, 255, 0), 2f);
    }

    public void ToTempleAnim()
    {
        StartCoroutine(EnterTempleRoutine());
    }

    IEnumerator EnterTempleRoutine()
    {
        levelUI.StartFade(Color.clear, Color.black, 2f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Temple");
    }
}
