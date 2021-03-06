﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image fadeScreen;

    // Fade screen color

    public void StartFade(Color oldColor, Color newColor, float duration)
    {
        StartCoroutine(FadeToColor(oldColor, newColor, duration));
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

    public void FadeText(Text text, Color oldColor, Color newColor, float duration)
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
