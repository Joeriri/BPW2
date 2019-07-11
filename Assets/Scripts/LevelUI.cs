using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image blackScreen;

    public void BlackScreenActivate(bool value)
    {
        blackScreen.enabled = value;
    }
    
    public void StartFadeToBlack(float duration)
    {
        StartCoroutine(FadeToBlack(duration));
    }

    IEnumerator FadeToBlack(float duration)
    {
        for (float i = 0f; i < duration; i += Time.deltaTime)
        {
            blackScreen.color = Color.Lerp(Color.clear, Color.black, i);
            yield return null;
        }
    }

    public void StartFadeToClear(float duration)
    {
        StartCoroutine(FadeToClear(duration));
    }

    IEnumerator FadeToClear(float duration)
    {
        for (float i = 0f; i < duration; i += Time.deltaTime)
        {
            blackScreen.color = Color.Lerp(Color.black, Color.clear, i);
            yield return null;
        }
    }
}
