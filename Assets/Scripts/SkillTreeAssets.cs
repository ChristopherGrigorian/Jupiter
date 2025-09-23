using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/SkillNode")]
public class SkillNodeData : ScriptableObject
{
    public string nodeId;
    public SkillData skill;                 // your existing SkillData
    public List<SkillNodeData> prerequisites;
    public int cost = 1;
}
