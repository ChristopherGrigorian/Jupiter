using UnityEngine;
using System.Collections.Generic;

public enum WeaponType { Gun, Sword }

[CreateAssetMenu(menuName = "Combat/Weapon")]

public class WeaponData : ScriptableObject
{
    public string weaponName;

    [TextArea(3, 10)]
    public string weaponDescription;

    public WeaponType weaponType;
    public List<SkillData> weaponSkills;
}
