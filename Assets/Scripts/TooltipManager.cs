using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private TextMeshProUGUI skillTreeTooltipText;
    [SerializeField] private TextMeshProUGUI shopTooltipText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        HideTooltip();
    }

    public void ShowTooltip(string content)
    {
        tooltipText.text = content;
        skillTreeTooltipText.text = content;
        shopTooltipText.text = content;
    }

    public void ShowToolTipCombat(TextMeshProUGUI textToChange, string content)
    {
        textToChange.text = content;
    }

    public void HideTooltip()
    {
        tooltipText.text = "";
        skillTreeTooltipText.text = "";
        shopTooltipText.text = "";
    }

    public void HideToolTipCombat(TextMeshProUGUI textToChange)
    {
        textToChange.text = "";
    }
}
