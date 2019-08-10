using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image fadeScreen;
    private Color oldColor;

    public void BlackScreenActivate(bool value)
    {
        fadeScreen.enabled = value;
    }

    public void StartFade(Color color, float duration)
    {
        oldColor = fadeScreen.color;
        StartCoroutine(FadeTo(color, duration));
    }

    IEnumerator FadeTo(Color newColor, float duration)
    {
        for (float i = 0f; i < duration; i += Time.deltaTime)
        {
            fadeScreen.color = Color.Lerp(oldColor, newColor, i);
            yield return null;
        }
    }
}
