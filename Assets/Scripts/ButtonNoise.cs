using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonNoise : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Button thisButton;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip pressedClip;

    [SerializeField] private bool menuButton = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!menuButton) thisButton.onClick.AddListener(() => PlayClickNoise());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverClip, 0.5f);
    }

    public void PlayClickNoise()
    {
        audioSource.PlayOneShot(pressedClip, 0.5f);
    }

    public void AddAudioSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }

    public void AddHoverClip(AudioClip audioClip)
    {
        this.hoverClip = audioClip;
    }

    public void AddPressedClip(AudioClip audioClip)
    {
        this.pressedClip = audioClip;
    }

    public void AssignSelf(Button self)
    {
        thisButton = self;
    }
}
