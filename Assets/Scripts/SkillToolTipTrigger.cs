using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SkillData skill;
    private TextMeshProUGUI text;

    public void Init(SkillData skillData, TextMeshProUGUI textToChange = null)
    {
        skill = skillData;
        text = textToChange; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string content = $"{skill.skillName}: {skill.skillDescription}\nType: {skill.type}\nPower: {skill.power}\nPotency: {skill.potency}\nMP Cost: {skill.MPCost}";
        if (text != null)
        {
            TooltipManager.Instance.ShowToolTipCombat(text, content);
        }
        else
        {
            TooltipManager.Instance.ShowTooltip(content);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        if (text != null)
        {
            TooltipManager.Instance.HideToolTipCombat(text);
        } else
        {
            TooltipManager.Instance.HideTooltip();
        }
    }
}
