using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFadeIn : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float fadeDuration = 0.5f;

    public void FadeInText(string text)
    {
        textMeshPro.text = text;
        // Setze die initiale Alpha-Deckkraft auf 0, um sicherzustellen, dass der Text von vollständig transparent einfadet
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0);
        textMeshPro.gameObject.SetActive(true);
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        float timer = 0f;
        Color initialColor = textMeshPro.color; // Sollte jetzt Alpha von 0 haben
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f); // Vollständige Deckkraft

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            textMeshPro.color = Color.Lerp(initialColor, targetColor, timer / fadeDuration);
            yield return null;
        }

        textMeshPro.color = targetColor; // Stelle sicher, dass die Deckkraft am Ende voll ist
    }
}
