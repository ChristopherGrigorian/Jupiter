using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private TextMeshProUGUI skillTreeTooltipText;

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
    }

    public void HideTooltip()
    {
        tooltipText.text = "";
        skillTreeTooltipText.text = "";
    }
}
