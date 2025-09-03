using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PedestalController : MonoBehaviour
{
    [SerializeField] private Slider hp;
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private CanvasGroup highlight;
    [SerializeField] private Image characterImage;

    private Combatant bound;   // from your game
    public Combatant Bound => bound;

    public void Bind(Combatant c)
    {
        bound = c;
        if (nameLabel) nameLabel.text = c.Name;
        if (hp)
        {
            hp.maxValue = c.data.maxHP;
            hp.value = Mathf.Clamp(c.currentHP, 0, c.data.maxHP);
            if (c.data.battleImage != null)
            {
                characterImage.sprite = c.data.battleImage;
            }
            print(hp.value);
        }
        SetHover(false);
    }

    public void Refresh()
    {
        print(bound.currentHP);
        if (bound is null || !hp) return;
        hp.maxValue = bound.data.maxHP;
        hp.value = Mathf.Clamp(bound.currentHP, 0, bound.data.maxHP);
        gameObject.SetActive(bound.IsAlive);
    }

    // Optional: billboard the bar to the camera
    void LateUpdate()
    {
        if (Camera.main)
        {
            nameLabel.transform.forward = Camera.main.transform.forward;
            hp.transform.forward = Camera.main.transform.forward;

            Vector3 hpEulerAngles = hp.transform.eulerAngles;
            characterImage.transform.rotation = Quaternion.Euler(0, hpEulerAngles.y, 0);
        }
    }

    public void SetHover(bool on)
    {
        if (highlight)
        {
            highlight.alpha = on ? 1f : 0f;
        }
        if (nameLabel)
        {
            nameLabel.color = on ? new Color(1f, 1f, 0.6f) : Color.white;
        }
    }
}
