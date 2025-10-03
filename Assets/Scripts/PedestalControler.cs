using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PedestalController : MonoBehaviour
{
    public Transform viewingPoint;

    [Header("Common UI")]
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private TextMeshProUGUI mp;
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private CanvasGroup highlight;
    [SerializeField] private Image characterImage;

    [Header("UI")]
    [SerializeField] private CanvasGroup uiGroup;
    [SerializeField] private UIFader fader;

    [Header("Player-only UI")]
    [SerializeField] private CanvasGroup actionExtras; // tabs, prompts, etc (optional)

    private Combatant bound;
    public Combatant Bound => bound;

    private bool isPlayer;

    public void Bind(Combatant c)
    {
        bound = c;
        isPlayer = c.isPlayerControlled;

        if (nameLabel) nameLabel.text = c.Name;

        if (characterImage && c.data.battleImage)
            characterImage.sprite = c.data.battleImage;

        // tell registrar who we are so it can register the HUD
        var reg = GetComponentInChildren<CharacterUIRegistrar>(true);
        if (reg) reg.RegisterFor(c);

        // hide player-only bits for enemies (your existing logic)
        if (actionExtras)
        {
            actionExtras.alpha = isPlayer ? 1f : 0f;
            actionExtras.interactable = isPlayer;
            actionExtras.blocksRaycasts = isPlayer;
            actionExtras.gameObject.SetActive(isPlayer);
        }

        UpdateStatText();
        SetHover(false);
        gameObject.SetActive(c.IsAlive);
    }

    public void Refresh()
    {
        if (bound is null) return;
        UpdateStatText();
        gameObject.SetActive(bound.IsAlive);
    }

    private void UpdateStatText()
    {
        if (bound == null) return;

        // clamp to avoid weird negatives/overs
        int curHP = Mathf.Clamp(bound.currentHP, 0, bound.EffectiveMaxHP);
        int maxHP = Mathf.Max(1, bound.EffectiveMaxHP);

        int curMP = Mathf.Max(0, bound.currentMP);
        int maxMP = Mathf.Max(0, bound.EffectiveMaxMP);

        if (hp) hp.text = $"HP: {curHP} / {maxHP}";
        if (mp) mp.text = $"MP: {curMP} / {maxMP}";
    }

    void LateUpdate()
    {   /*
        if (!Camera.main) return;

        // billboard texts toward camera
        if (nameLabel) nameLabel.transform.forward = Camera.main.transform.forward;
        if (hp) hp.transform.forward = Camera.main.transform.forward;
        if (mp) mp.transform.forward = Camera.main.transform.forward;

        // keep portrait facing same yaw as text (optional)
        if (characterImage && hp)
        {
            var e = hp.transform.eulerAngles;
            characterImage.transform.rotation = Quaternion.Euler(0f, e.y, 0f);
        }
        */
    }

    public void SetHover(bool on)
    {
        if (highlight) highlight.alpha = on ? 1f : 0f;
        if (nameLabel) nameLabel.color = on ? new Color(1f, 1f, 0.6f) : Color.white;
    }

    public void SetUIVisible(bool visible, bool instant = false)
    {
        // Prefer UIFader if assigned
        if (fader)
        {
            if (instant) fader.SetVisibleImmediate(visible);
            else if (visible) fader.Show();
            else fader.Hide();
            return;
        }

        // Fallback to CanvasGroup if no UIFader
        if (!uiGroup) uiGroup = GetComponentInChildren<CanvasGroup>(true);
        if (!uiGroup) return;

        if (instant)
        {
            uiGroup.alpha = visible ? 1f : 0f;
            uiGroup.interactable = visible;
            uiGroup.blocksRaycasts = visible;
            uiGroup.gameObject.SetActive(visible);
        }
        else
        {
            // no fader = no animation; do immediate anyway
            uiGroup.alpha = visible ? 1f : 0f;
            uiGroup.interactable = visible;
            uiGroup.blocksRaycasts = visible;
            uiGroup.gameObject.SetActive(visible);
        }
    }
}
