using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SkillData skill;

    public void Init(SkillData skillData)
    {
        skill = skillData;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string content = $"{skill.skillName}: {skill.skillDescription}\nType: {skill.type}\nPower: {skill.power}\nPotency: {skill.potency}\nMP Cost: {skill.MPCost}";
        TooltipManager.Instance.ShowTooltip(content);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideTooltip();
    }
}
