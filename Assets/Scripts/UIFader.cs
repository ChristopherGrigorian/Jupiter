using UnityEngine;
using System.Collections;

public class UIFader : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeTo(0f));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeTo(1f));
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
