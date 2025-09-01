using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("The Image component to show/hide")]
    [SerializeField] private Image targetImage;

    [Header("The Sprite to show on hover")]
    [SerializeField] private Sprite hoverSprite;

    private Sprite originalSprite;

    void Start()
    {
        if (targetImage != null)
            originalSprite = targetImage.sprite;
    }

    // Called when mouse/pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null && hoverSprite != null)
        {
            targetImage.sprite = hoverSprite;
            targetImage.enabled = true; // make sure it's visible
        }
    }

    // Called when mouse/pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            targetImage.sprite = originalSprite;
        }
    }
}

