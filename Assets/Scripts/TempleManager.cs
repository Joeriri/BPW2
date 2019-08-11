using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TempleManager : MonoBehaviour
{
    // Refs
    private LevelUI levelUI;
    [SerializeField] private Image fadeScreen;
    [SerializeField] private Text storyText;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc;

    // Vars
    [SerializeField] private float fadeInDuration = 2f;
    [SerializeField] private float solveWaitDuration = 2f;
    [SerializeField] private float fadeOutDuration = 2f;
    [SerializeField] private float textFadeDuration = 2f;
    [SerializeField] private float textWaitDuration = 2f;

    private void Awake()
    {
        levelUI = FindObjectOfType<LevelUI>();
        fpc = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    void Start()
    {
        StartCoroutine(EnterTempleRoutine());
    }

    // Enter Temple sequence

    IEnumerator EnterTempleRoutine()
    {
        fpc.enabled = false;
        fadeScreen.color = Color.black;
        // Text fade in
        levelUI.FadeText(storyText, Color.clear, Color.white, textFadeDuration);
        yield return new WaitForSeconds(textFadeDuration);
        // Wait
        yield return new WaitForSeconds(textWaitDuration);
        // Text fade out
        levelUI.FadeText(storyText, Color.white, Color.clear, textFadeDuration);
        yield return new WaitForSeconds(textFadeDuration);
        // Fade in
        levelUI.StartFade(Color.black, Color.clear, fadeInDuration);
        fpc.enabled = true;
    }

    // Exit Temple sequence

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
