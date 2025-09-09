using UnityEngine;

[System.Serializable]
public struct CharacterStatsSnapshot
{
    public int level, experience, maxHP, maxMP, strength, perception, evasiveness, spirit, speed;

    public static CharacterStatsSnapshot Capture(CharacterData c) => new CharacterStatsSnapshot
    {
        level = c.level,
        experience = c.experience,
        maxHP = c.maxHP,
        maxMP = c.maxMP,
        strength = c.strength,
        perception = c.perception,
        evasiveness = c.evasiveness,
        spirit = c.spirit,
        speed = c.speed
    };
}

public class LevelUpSummary
{
    public CharacterData character;
    public int xpGained;
    public CharacterStatsSnapshot before;
    public CharacterStatsSnapshot after;
    public int xpNeededToNext;
    public int LevelsGained => after.level - before.level;
}

