using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/SkillTree")]
public class SkillTreeData : ScriptableObject
{
    public List<SkillNodeData> nodes;
    public SkillNodeData Get(string id) => nodes.Find(n => n.nodeId == id);
}