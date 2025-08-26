using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkillNodeButton : MonoBehaviour
{
    [SerializeField] private string nodeId;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI label;

    private SkillTreeData tree;
    private SkillNodeData node;
    private CharacterData character;

    public void Bind(SkillTreeData tree, CharacterData character)
    {
        this.tree = tree;
        this.character = character;
        node = tree != null ? tree.Get(nodeId) : null;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        Refresh();
    }

    void Refresh()
    {
        if (tree == null || node == null || character == null)
        {
            if (label) label.text = nodeId + " (Missing)";
            button.interactable = false;
            return;
        }

        bool unlocked = character.IsNodeUnlocked(node.nodeId);
        bool canUnlock = character.CanUnlock(node);
        bool equipped = node.skill != null && (character.EquippedSkills?.Contains(node.skill) ?? false);

        if (equipped)
        {
            if (label) label.text = $"{node.skill.skillName} (Equipped)";
            button.interactable = true;
        }
        else if (unlocked)
        {
            if (label) label.text = node.skill != null ? node.skill.skillName : node.nodeId;
            button.interactable = true;
        }
        else if (canUnlock)
        {
            if (label) label.text = $"{node.skill.skillName} (Unlock {node.cost})";
            button.interactable = true;
        }
        else
        {
            if (label) label.text = $"{(node.skill ? node.skill.skillName : node.nodeId)} (Locked)";
            button.interactable = false;
        }
    }

    void OnClick()
    {
        if (node == null || character == null) return;

        if (!character.IsNodeUnlocked(node.nodeId))
        {
            // Try unlock
            if (!character.Unlock(node))
                return;
        }
        else
        {
            // Toggle equip
            if (node.skill != null)
            {
                if (!character.ToggleEquip(node.skill))
                    Debug.Log("Equip cap reached (4) or skill not unlocked.");
            }
        }

        // Ask the parent to refresh the whole view so other buttons update
        GetComponentInParent<SkillTreeController>()?.RefreshAll();
    }
}
