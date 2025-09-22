using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private WeaponData weapon;

    public void Init(WeaponData weaponData)
    {
        weapon = weaponData;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var sb = new StringBuilder();
        int lvl = InventoryManager.Instance.GetWeaponLevel(weapon);
        int pwr = InventoryManager.Instance.GetWeaponPower(weapon);
        sb.AppendLine($"{weapon.weaponName}");
        sb.AppendLine($"Weapon Type: {weapon.weaponType}");
        sb.AppendLine(weapon.weaponDescription);
        sb.AppendLine($"Current Known Level: {lvl}");
        sb.AppendLine($"Current Power: {pwr}");
        sb.AppendLine("Current Skills:");
        if (weapon.weaponSkills != null && weapon.weaponSkills.Count > 0)
        {
            foreach (var s in weapon.weaponSkills)
            {
                sb.AppendLine($" - {s?.skillName ?? "(missing)"}");
            }
        } else
        {
            sb.AppendLine(" - (none)");
        }
        string content = sb.ToString();
        TooltipManager.Instance.ShowTooltip(content);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideTooltip();
    }
}
