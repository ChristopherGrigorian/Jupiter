using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Combat/Character")]
public class CharacterData : ScriptableObject
{
    public string id;
    public string characterName;
    public int level = 1;
    public int experience;
    public int maxHP;
    public int maxMP;
    public int strength;
    public int perception;
    public int evasiveness;
    public int spirit;
    public int speed;
    public WeaponData EquippedWeapon;
    public AudioClip dodgeSound;
    public bool currentlyEquipped = false;

    public List<WeaponType> equipableWeaponTypes;
    public List<SkillData> Skills;
    public List<Sprite> Images;
    public float xPos;
    public float yPos;
    public Sprite battleImage;
    public List<Sprite> entranceImages;
    public List<Sprite> hoverImages;
    public List<WeaponData> droppableWeapons;
    public int heldCoin = 0;

    public SkillTreeData skillTree;

    [SerializeField] private List<string> unlockedNodeIds = new(); // nodeId strings
    [SerializeField] public List<SkillData> equippedSkills = new();
    public const int EquippedSkillCap = 4;

    public int skillPoints;

    public bool IsNodeUnlocked(string nodeId) => unlockedNodeIds.Contains(nodeId);
    public IReadOnlyList<SkillData> EquippedSkills => equippedSkills;

    public bool CanUnlock(SkillNodeData node)
    {
        if (node == null) return false;
        if (IsNodeUnlocked(node.nodeId)) return false;
        if (skillTree == null) return false;

        // prereqs
        foreach (var pre in node.prerequisites)
            if (!unlockedNodeIds.Contains(pre.nodeId)) return false;

        // cost
        if (skillPoints < node.cost) return false;

        return true;
    }

    public bool Unlock(SkillNodeData node)
    {
        if (!CanUnlock(node)) return false;
        unlockedNodeIds.Add(node.nodeId);
        skillPoints -= node.cost;
        if (node.skill != null && !Skills.Contains(node.skill))
            Skills.Add(node.skill); // if you want “unlocked” skills to appear in your general pool
        return true;
    }

    public bool ToggleEquip(SkillData skill)
    {
        if (skill == null) return false;

        // Only allow equip if it’s unlocked (either baseline or via a node that’s unlocked)
        if (!Skills.Contains(skill)) return false;

        if (equippedSkills.Contains(skill))
        {
            equippedSkills.Remove(skill);
            return true;
        }

        if (equippedSkills.Count >= EquippedSkillCap) return false;
        equippedSkills.Add(skill);
        return true;
    }

    // handy utility if you ever need to prune or fix
    public void EnsureEquipCap()
    {
        if (equippedSkills.Count > EquippedSkillCap)
            equippedSkills.RemoveRange(EquippedSkillCap, equippedSkills.Count - EquippedSkillCap);
    }

    public void GainExperience(int amount)
    {
        experience += amount;
        while (experience >= GetRequiredXPForLevel(level + 1))
        {
            experience -= GetRequiredXPForLevel(level + 1);
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        maxHP += Mathf.RoundToInt(10 + level * 2f);                
        strength += Mathf.RoundToInt(1 + level * 0.5f);
        perception += Mathf.RoundToInt(1 + level * 0.3f);
        evasiveness += Mathf.RoundToInt(1 + level * 0.3f);
        spirit += Mathf.RoundToInt(1 + level * 0.5f);
        speed += Mathf.RoundToInt(1 + level * 0.4f);
        Debug.Log($"{characterName} leveled up to {level}!");
    }

    public int GetRequiredXPForLevel(int targetLevel)
    {
        return Mathf.FloorToInt(100 * Mathf.Pow(targetLevel, 1.5f)); 
    }

    public int XPNeededToNextLevel()
    {
        int nextLevelCost = GetRequiredXPForLevel(level + 1);
        return Mathf.Max(0, nextLevelCost - experience);
    }
}
