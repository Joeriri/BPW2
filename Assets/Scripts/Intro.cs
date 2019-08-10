using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private Text introText;

    [SerializeField] private float startWaitDuration = 1f;
    [SerializeField] private float startFadeDuration = 2f;
    [SerializeField] private float introFadeDuration = 2f;
    [SerializeField] private float introWaitDuration = 1f;
    [SerializeField] private float introTextFadeDuration = 4f;
    [SerializeField] private float introTextWaitDuration = 4f;

    private void Start()
    {
        StartCoroutine(StartRoutine());
    }

    // Start sequence
    IEnumerator StartRoutine()
    {
        // Enable Fadescreen
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.color = Color.black;
        // Wait
        yield return new WaitForSeconds(startWaitDuration);
        // Fade in
        StartFade(Color.clear, startFadeDuration);
        yield return new WaitForSeconds(startFadeDuration);
        // Disable Fadescreen
        fadeScreen.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Intro sequence
    public void StartIntro()
    {
        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine()
    {
        // Fade in
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        fadeScreen.gameObject.SetActive(true);
        StartFade(Color.white, introFadeDuration);
        yield return new WaitForSeconds(introFadeDuration);
        // Wait
        yield return new WaitForSeconds(introWaitDuration);
        // Text fade in
        introText.gameObject.SetActive(true);
        FadeText(introText, new Color(50, 50, 50, 0), new Color(50, 50, 50), introTextFadeDuration);
        // Wait
        yield return new WaitForSeconds(introTextWaitDuration);
        // Text fade out
        FadeText(introText, new Color(50, 50, 50), new Color(50, 50, 50, 0), introTextFadeDuration);
        introText.gameObject.SetActive(false);
        // Wait
        yield return new WaitForSeconds(2f);
        // Go to Desert scene
        SceneManager.LoadScene("SampleScene");
        fadeScreen.gameObject.SetActive(false);
    }

    // Fade screen color

    public void StartFade(Color newColor, float duration)
    { 
        StartCoroutine(FadeToColor(fadeScreen.color, newColor, duration));
    }

    IEnumerator FadeToColor(Color oldColor, Color newColor, float duration)
    {
        float step = 1f / duration;
        for (float i = 0f; i < 1f; i += step * Time.deltaTime)
        {
            fadeScreen.color = Color.Lerp(oldColor, newColor, i);
            yield return null;
        }
    }

    // Fade Text color

    private void FadeText(Text text, Color oldColor, Color newColor, float duration)
    {
        StartCoroutine(FadeTextRoutine(text, oldColor, newColor, duration));
    }

    IEnumerator FadeTextRoutine(Text text, Color oldColor, Color newColor, float duration)
    {
        float step = 1f / duration;
        for (float i = 0f; i < 1f; i += step * Time.deltaTime)
        {
            text.color = Color.Lerp(oldColor, newColor, i);
            yield return null;
        }
    }
}
