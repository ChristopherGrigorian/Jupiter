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

[CreateAssetMenu(menuName = "Combat/SkillTree")]
public class SkillTreeData : ScriptableObject
{
    public List<SkillNodeData> nodes;
    public SkillNodeData Get(string id) => nodes.Find(n => n.nodeId == id);
}
