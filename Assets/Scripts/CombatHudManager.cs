using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatHudManager : MonoBehaviour
{
    [Header("Containers")]
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private Transform skillContainer;
    [SerializeField] private Transform itemContainer;

    [Header("Prefabs")]
    [SerializeField] private GameObject weaponButtonPrefab;
    [SerializeField] private GameObject skillButtonPrefab;
    [SerializeField] private GameObject itemButtonPrefab;

    [Header("Tabs")]
    [SerializeField] private Button weaponTabButton;
    [SerializeField] private Button skillTabButton;
    [SerializeField] private Button itemTabButton;

    private CharacterData activeCharacter;
    private System.Action<SkillData> cachedSkillCallback;
    private bool _wired;

    void Awake()
    {
        WireIfNeeded(); // Awake runs even if GO is inactive
    }

    void OnEnable()
    {
        WireIfNeeded(); // just in case the GO was created already
    }

    private void WireIfNeeded()
    {
        if (_wired) return;

        // Optional: auto-find if not assigned in prefab
        if (!weaponContainer) weaponContainer = transform.Find("Weapon");
        if (!skillContainer) skillContainer = transform.Find("Skills");
        if (!itemContainer) itemContainer = transform.Find("Items");

        if (weaponTabButton) { weaponTabButton.onClick.RemoveAllListeners(); weaponTabButton.onClick.AddListener(() => ShowTab("Weapon")); }
        if (skillTabButton) { skillTabButton.onClick.RemoveAllListeners(); skillTabButton.onClick.AddListener(() => ShowTab("Skill")); }
        if (itemTabButton) { itemTabButton.onClick.RemoveAllListeners(); itemTabButton.onClick.AddListener(() => ShowTab("Item")); }

        _wired = true;
    }

    public void PopulateActions(CharacterData character, System.Action<SkillData> onSkillChosen)
    {
        WireIfNeeded(); // ensure references exist
        activeCharacter = character;
        cachedSkillCallback = onSkillChosen;

        // Safety: if any required refs are missing, bail with a clear log
        if (!weaponContainer || !skillContainer || !itemContainer)
        {
            Debug.LogError("[CombatHud] Containers are not assigned on prefab.");
            return;
        }
        if (!weaponButtonPrefab || !skillButtonPrefab || !itemButtonPrefab)
        {
            Debug.LogError("[CombatHud] Button prefabs are not assigned on prefab.");
            return;
        }

        ShowTab("Weapon");
    }

    private void ShowTab(string type)
    {
        // extra protection against nulls
        if (!weaponContainer || !skillContainer || !itemContainer)
        {
            Debug.LogError("[CombatHud] Missing containers; cannot ShowTab.");
            return;
        }

        weaponContainer.gameObject.SetActive(type == "Weapon");
        skillContainer.gameObject.SetActive(type == "Skill");
        itemContainer.gameObject.SetActive(type == "Item");

        ClearAll();

        if (type == "Weapon" && activeCharacter && activeCharacter.EquippedWeapon)
        {
            foreach (var skill in activeCharacter.EquippedWeapon.weaponSkills)
            {
                var btnGO = MakeActionButton(
                    weaponContainer,
                    weaponButtonPrefab,
                    skill.skillName,
                    () => { cachedSkillCallback?.Invoke(skill); }
                );

                var trigger = btnGO.AddComponent<SkillTooltipTrigger>();
                trigger.Init(skill);
            }
        }
        else if (type == "Skill" && activeCharacter)
        {
            foreach (var skill in activeCharacter.EquippedSkills)
            {
                var btnGO = MakeActionButton(
                    skillContainer,
                    skillButtonPrefab,
                    skill.skillName,
                    () => { cachedSkillCallback?.Invoke(skill); }
                );

                var trigger = btnGO.AddComponent<SkillTooltipTrigger>();
                trigger.Init(skill);
            }
        }
        else if (type == "Item")
        {
            foreach (var item in InventoryManager.Instance.items)
            {
                var btnGO = MakeActionButton(
                    itemContainer,
                    itemButtonPrefab,
                    item.itemName,
                    () =>
                    {
                        cachedSkillCallback?.Invoke(item.skillAttached);
                        item.DestorySelf();
                    }
                );

                var trigger = btnGO.AddComponent<SkillTooltipTrigger>();
                trigger.Init(item.skillAttached);
            }
        }

    }

    private void ClearAll()
    {
        if (weaponContainer) foreach (Transform c in weaponContainer) Destroy(c.gameObject);
        if (skillContainer) foreach (Transform c in skillContainer) Destroy(c.gameObject);
        if (itemContainer) foreach (Transform c in itemContainer) Destroy(c.gameObject);
    }

    public void SetAllActionButtonsInteractable(bool interactable)
    {
        SetButtonsInContainer(weaponContainer, interactable);
        SetButtonsInContainer(skillContainer, interactable);
        SetButtonsInContainer(itemContainer, interactable);
    }

    private void SetButtonsInContainer(Transform container, bool interactable)
    {
        if (!container) return;
        foreach (Transform child in container)
        {
            var b = child.GetComponent<Button>();
            if (b) b.interactable = interactable;
        }
    }
    private Button FindButtonOn(GameObject go)
    {
        // Try root first, then any child (including inactive)
        var btn = go.GetComponent<Button>();
        if (!btn) btn = go.GetComponentInChildren<Button>(true);
        return btn;
    }

    private GameObject MakeActionButton(Transform parent, GameObject prefab, string label, System.Action onClick)
    {
        var btnGO = Instantiate(prefab, parent);
        var tmp = btnGO.GetComponentInChildren<TextMeshProUGUI>(true);
        if (tmp) tmp.text = label;

        var btn = FindButtonOn(btnGO);
        if (!btn)
        {
            Debug.LogError($"[CombatHud] Prefab '{prefab.name}' has no Button component on itself or its children.");
            return btnGO; // still spawn so you can see it, but it won't be clickable
        }

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => onClick?.Invoke());
        return btnGO;
    }

}
