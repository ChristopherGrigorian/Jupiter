using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterHoverImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("The Image component to show/hide")]
    public Image targetImage;

    [Header("The Sprite to show on hover")]
    public List<Sprite> entranceSprites;
    public List<Sprite> hoverSprites;

    private Sprite originalSprite;

    void Start()
    {
        if (targetImage != null)
            originalSprite = targetImage.sprite;
    }

    // Called when mouse/pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null && hoverSprites != null)
        {
            targetImage.enabled = true; 
            if (entranceFlipbookCoroutine != null)
                StopCoroutine(entranceFlipbookCoroutine);
            if (hoverFlipbookCoroutine != null)
                StopCoroutine(hoverFlipbookCoroutine);


            entranceFlipbookCoroutine = StartCoroutine(StartEntranceThenHover());
        }
    }

    // Called when mouse/pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            if (entranceFlipbookCoroutine != null)
                StopCoroutine(entranceFlipbookCoroutine);

            if (hoverFlipbookCoroutine != null)
                StopCoroutine(hoverFlipbookCoroutine);

            targetImage.sprite = originalSprite;
            targetImage.enabled = false;
        }
    }

    private Coroutine entranceFlipbookCoroutine;
    private Coroutine hoverFlipbookCoroutine;

    private IEnumerator StartEntranceThenHover()
    {
        yield return new WaitForSeconds(0.15f);
        yield return StartCoroutine(EntranceFlipbookRoutine(entranceSprites));
        hoverFlipbookCoroutine = StartCoroutine(HoverFlipbookRoutine(hoverSprites));
    }

    private IEnumerator EntranceFlipbookRoutine(List<Sprite> images)
    {
        if (images == null || images.Count == 0)
            yield break;

        foreach (var sprite in images)
        {
            targetImage.sprite = sprite;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator HoverFlipbookRoutine(List<Sprite> images)
    {
        if (images == null || images.Count == 0)
            yield break;

        int index = 0;
        while (true)
        {
            targetImage.sprite = images[index];
            index = (index + 1) % images.Count;
            yield return new WaitForSeconds(0.25f);
        }
    }
}

