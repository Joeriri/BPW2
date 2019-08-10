using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempleManager : MonoBehaviour
{
    // Refs
    private LevelUI levelUI;

    // Vars
    [SerializeField] private float fadeInDuration = 2f;
    [SerializeField] private float solveWaitDuration = 2f;
    [SerializeField] private float fadeOutDuration = 2f;

    private void Awake()
    {
        levelUI = FindObjectOfType<LevelUI>();
    }

    void Start()
    {
        levelUI.StartFade(Color.black, Color.clear, fadeInDuration);
    }

    public void ExitTemple()
    {
        StartCoroutine(ExitTempleRoutine());
    }

    IEnumerator ExitTempleRoutine()
    {
        // Wait
        yield return new WaitForSeconds(solveWaitDuration);
        // Fade out
        levelUI.StartFade(Color.clear, Color.black, fadeOutDuration);
        yield return new WaitForSeconds(fadeOutDuration);
        // Go to End scene
        SceneManager.LoadScene("End");
    }

}
