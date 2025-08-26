using System.Collections.Generic;
using System.Reflection;
using Ink.Parsed;
using UnityEngine;

public class SkillTreeController : MonoBehaviour
{
    [SerializeField] private SkillNodeButton buttonPrefab;
    [SerializeField] private Transform buttonContainer;

    private CharacterData boundCharacter;
    private SkillTreeData boundTree;
    private readonly List<SkillNodeButton> nodeButtons = new();

    private static FieldInfo nodeIdField;

    void Awake()
    {
        nodeIdField ??= typeof(SkillNodeButton)
            .GetField("nodeId", BindingFlags.Instance | BindingFlags.NonPublic);

        if (nodeIdField == null)
            Debug.LogError("[SkillTreeController] Couldn't find private field 'nodeId' on SkillNodeButton.");
    }

    public void Bind(CharacterData character)
    {
        if (character == null) { Debug.LogError("[SkillTreeController] Bind: character NULL", this); return; }

        boundCharacter = character;
        boundTree = character.skillTree != null ? character.skillTree : null;
        
        if (boundTree == null)
        {
            Debug.LogError("[SkillTreeController] Bind: NO TREE", this);
            return;
        }

        for (int i = buttonContainer.childCount - 1; i >= 0; i--)
            Destroy(buttonContainer.GetChild(i).gameObject);
        nodeButtons.Clear();

        // spawn one button per node with its own nodeId
        foreach (var node in boundTree.nodes) // assumes List<SkillNodeData> nodes
        {
            var btn = Instantiate(buttonPrefab, buttonContainer);

            // set unique nodeId on this instance BEFORE Bind()
            if (nodeIdField != null)
                nodeIdField.SetValue(btn, node.nodeId);

            // now the button can resolve tree.Get(nodeId) internally
            btn.Bind(boundTree, boundCharacter);

            var trigger = btn.GetComponent<SkillTooltipTrigger>();
            if (trigger == null) trigger = btn.gameObject.AddComponent<SkillTooltipTrigger>();
            if (node.skill != null) trigger.Init(node.skill);  // safe even if null

            nodeButtons.Add(btn);
        }

        Debug.Log($"[SkillTreeController] Spawned {nodeButtons.Count} node buttons for {character.name}", this);
    }


    public void RefreshAll()
    {
        foreach (var btn in nodeButtons)
            btn.SendMessage("Refresh", SendMessageOptions.DontRequireReceiver);
    }

}
