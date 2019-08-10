using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.color = Color.black;
        yield return new WaitForSeconds(startWaitDuration);
        StartFade(Color.clear, startFadeDuration);
        yield return new WaitForSeconds(startFadeDuration);
        fadeScreen.gameObject.SetActive(false);
    }

    // Intro sequence
    public void StartIntro()
    {
        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine()
    {
        fadeScreen.gameObject.SetActive(true);
        StartFade(Color.white, introFadeDuration);
        yield return new WaitForSeconds(introFadeDuration);
        yield return new WaitForSeconds(introWaitDuration);
        // Narrative
        introText.gameObject.SetActive(true);
        introText.color = Color.clear;

        float step = 1f / introTextFadeDuration;
        for (float i = 0f; i < 1f; i += step * Time.deltaTime)
        {
            introText.color = Color.Lerp(Color.clear, Color.black, i);
            yield return null;
        }

        yield return new WaitForSeconds(introTextWaitDuration);

        float step2 = 1f / introTextFadeDuration;
        for (float i = 0f; i < 1f; i += step * Time.deltaTime)
        {
            introText.color = Color.Lerp(Color.black, Color.clear, i);
            yield return null;
        }
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
}
