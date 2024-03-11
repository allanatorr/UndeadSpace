using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFadeInOut : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float fadeDuration = 1.0f;
    public float displayTime = 2.0f;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
    }

    public void DisplayText(string text)
    {
        textMeshPro.gameObject.SetActive(true);
        textMeshPro.text = text;
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        textMeshPro.gameObject.SetActive(true);

        // Fade in
        yield return Fade(0, 1, fadeDuration);

        // Display time
        yield return new WaitForSeconds(displayTime);

        // Fade out
        yield return Fade(1, 0, fadeDuration);

        textMeshPro.gameObject.SetActive(false);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, alpha);
            yield return null;
        }
    }
}
