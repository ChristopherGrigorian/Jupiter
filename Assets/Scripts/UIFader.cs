using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class UIFader : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Coroutine runningFade;
    private Vector3 originalScale;

    [Header("Fade")]
    public float fadeDuration = 0.35f;

    [Header("Pop (scale)")]
    [SerializeField] private RectTransform popTarget;      // usually the same object’s RectTransform
    [SerializeField] private bool usePop = true;
    [SerializeField] private float popDuration = 0.28f;
    [SerializeField]
    private AnimationCurve popCurve =
        AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Flash Sweep (left→right)")]
    [Tooltip("Optional child Image used as the sweeping flash. Should be a white→transparent gradient (left to right).")]
    [SerializeField] private CanvasGroup flashCanvasGroup; // put on the flash image object
    [SerializeField] private RectTransform flashRect;      // rect of the flash image object
    [SerializeField] private bool useFlash = true;
    [SerializeField] private float flashDuration = 0.28f;
    [SerializeField] private float flashOvershoot = 0.15f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (!popTarget) popTarget = transform as RectTransform;
        originalScale = popTarget ? popTarget.localScale : Vector3.one;

        // If this HUD starts hidden in the scene, make sure alpha = 0
        if (!gameObject.activeInHierarchy) canvasGroup.alpha = 0f;
    }

    public void Show()
    {
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        if (!isActiveAndEnabled) { SetVisibleImmediate(true); return; }
        StartTransition(true);
    }

    public void Hide()
    {
        if (!isActiveAndEnabled) { SetVisibleImmediate(false); return; }
        StartTransition(false);
    }

    private void StartTransition(bool visible)
    {
        if (runningFade != null) StopCoroutine(runningFade);
        runningFade = StartCoroutine(DoTransition(visible));
    }

    private IEnumerator DoTransition(bool visible)
    {
        float fadeFrom = canvasGroup.alpha;
        float fadeTo = visible ? 1f : 0f;

        // Kick off optional effects
        Coroutine pop = null;
        Coroutine flash = null;

        if (visible)
        {
            if (usePop && popTarget) pop = StartCoroutine(PopIn());
            if (useFlash && flashCanvasGroup && flashRect) flash = StartCoroutine(FlashSweep());
        }

        // Fade
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            canvasGroup.alpha = Mathf.Lerp(fadeFrom, fadeTo, t);
            yield return null;
        }
        canvasGroup.alpha = fadeTo;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;

        // Wait for pop/flash if they’re running
        if (pop != null) yield return pop;
        if (flash != null) yield return flash;

        if (!visible)
        {
            // Reset scale when hiding so next Show starts clean
            if (popTarget) popTarget.localScale = originalScale;
            gameObject.SetActive(false);
        }
    }

    private IEnumerator PopIn()
    {
        // OutBack-ish curve without needing DOTween
        // Start a touch smaller, overshoot slightly, settle to 1
        Vector3 start = originalScale * 0.9f;
        Vector3 end = originalScale;
        Vector3 overshoot = originalScale * 1.05f;

        float half = popDuration * 0.6f; // go to overshoot first
        float elapsed = 0f;

        // Phase 1: 0.9 -> 1.05
        while (elapsed < half)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / half);
            // ease out
            float e = 1f - Mathf.Pow(1f - t, 3f);
            popTarget.localScale = Vector3.Lerp(start, overshoot, e);
            yield return null;
        }

        // Phase 2: 1.05 -> 1.00
        elapsed = 0f;
        float remain = popDuration - half;
        while (elapsed < remain)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / remain);
            // ease in-out
            float e = t * t * (3f - 2f * t);
            popTarget.localScale = Vector3.Lerp(overshoot, end, e);
            yield return null;
        }
        popTarget.localScale = end;
    }

    private IEnumerator FlashSweep()
    {
        // Assumes the flash image is a narrow vertical gradient (white→transparent) stretched full height of HUD,
        // anchored middle-left, and we animate its anchoredPosition.x from left offscreen to right offscreen.

        // Prep
        flashCanvasGroup.alpha = 1f;

        // compute width of the parent rect
        RectTransform parent = transform as RectTransform;
        float width = parent ? parent.rect.width : 1000f;

        float startX = -width * (0.5f + flashOvershoot);
        float endX = +width * (0.5f + flashOvershoot);
        Vector2 pos = flashRect.anchoredPosition;
        pos.x = startX;
        flashRect.anchoredPosition = pos;

        float elapsed = 0f;
        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / flashDuration);

            // move left -> right
            float x = Mathf.Lerp(startX, endX, t);
            flashRect.anchoredPosition = new Vector2(x, flashRect.anchoredPosition.y);

            // quick fade out near the end
            float fadeT = Mathf.Clamp01(1f - (t * 1.2f - 0.2f)); // starts fading after ~60%
            flashCanvasGroup.alpha = fadeT;

            yield return null;
        }

        // cleanup
        flashCanvasGroup.alpha = 0f;
    }

    // Add inside UIFader
    public void SetVisibleImmediate(bool visible)
    {
        if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
        gameObject.SetActive(true);
        canvasGroup.alpha = visible ? 1f : 0f;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
        if (!visible) gameObject.SetActive(false);
    }

}
