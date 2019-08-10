using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    // Refs
    [SerializeField] private Text outroText;

    // Vars
    [SerializeField] private float beginWaitDuration = 2f;
    [SerializeField] private float textFadeDuration = 3f;
    [SerializeField] private float textWaitDuration = 3f;
    [SerializeField] private float endWaitDuration = 2f;

    void Start()
    {
        StartCoroutine(EndRoutine());
    }

    IEnumerator EndRoutine()
    {
        // Wait
        yield return new WaitForSeconds(beginWaitDuration);
        // Text fade in
        FadeText(outroText, new Color(205, 205, 205, 0), new Color(205, 205, 205), textFadeDuration);
        // Wait
        yield return new WaitForSeconds(textWaitDuration);
        // Text fade out
        FadeText(outroText, new Color(205, 205, 205), new Color(205, 205, 205, 0), textFadeDuration);
        // Wait
        yield return new WaitForSeconds(endWaitDuration);
        // Go to Title Menu scene
        SceneManager.LoadScene("TitleMenu");
    }

    // Text fade

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
