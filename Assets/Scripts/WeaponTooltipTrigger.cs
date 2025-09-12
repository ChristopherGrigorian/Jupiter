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
        string content = $"{weapon.weaponName}\nWeapon Type: {weapon.weaponType}\n{weapon.weaponDescription}";
        TooltipManager.Instance.ShowTooltip(content);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideTooltip();
    }
}
