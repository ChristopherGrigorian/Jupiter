using System.Collections.Generic;
using UnityEngine;

public static class XPUtility
{
    public static int CalculateXPDrop(CharacterData enemy)
    {
        int baseXP = 20;
        float scalingFactor = 1.2f;
        float statSum = enemy.maxHP + enemy.maxMP + enemy.strength * 2 + enemy.spirit * 1.5f +
                        enemy.speed + enemy.perception + enemy.evasiveness;
        return Mathf.RoundToInt(baseXP + (enemy.level * scalingFactor) + statSum * 0.1f);
    }

    public static void AwardXPToParty(List<Combatant> defeatedEnemies, List<Combatant> party)
    {
        int totalXP = 0;
        foreach (Combatant enemy in defeatedEnemies)
        {
            int xp = CalculateXPDrop(enemy.data);
            totalXP += xp;
            Debug.Log($"Enemy {enemy.data.characterName} dropped {xp} XP");
        }

        int xpPerMember = totalXP / party.Count;
        foreach (Combatant member in party)
        {
            member.data.GainExperience(xpPerMember);
            Debug.Log($"{member.data.characterName} gains {xpPerMember} XP!");
        }
    }

}
