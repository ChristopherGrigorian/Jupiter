using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public List<WeaponData> weapons;

    [SerializeField] private List<WeaponData> acquireableWeapons;

    [SerializeField] public List<ItemData> items;
    [SerializeField] private List<ItemData> acquireableItems;

    public int totalCoin = 0;

    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color selectedColor = Color.red;

    private Button currentlySelectedBtn;

    [Header("Containers")]
    public Transform inventoryFeaturesContainer;
    public Transform characterSelectionContainer;
    public Transform equippableWeaponsContainer;
    public Transform skillTreeContainer;

    [Header("Prefabs")]
    public GameObject characterSelectionPrefab;
    public GameObject equippableWeaponPrefab;

    [Header("Pre-Established Inventory Features / Buttons")]
    public Button inventoryButton;
    public Button weaponInventoryButton;
    public Button skillTreeButton;

    [Header("Button Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip pressedClip;

    private string previousType = "";

    public Dictionary<WeaponData, WeaponProgress> weaponProgress = new();

    public bool revealedInventory = false;

    [SerializeField] private GameObject featuresHUD;
    public static InventoryManager Instance;

    private CharacterData currentSelectedCharacter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Optional safety
            return;
        }
    }

    private void Start()
    {
        inventoryButton.onClick.AddListener(() => ShowTab("Features"));
        weaponInventoryButton.onClick.AddListener(() => ShowTab("CharacterSelectorWeapons"));
        skillTreeButton.onClick.AddListener(() => ShowTab("CharacterSelectorSkillTree"));

        foreach (var w in weapons)
        {
            if (w == null) continue;
            if (!weaponProgress.ContainsKey(w))
                weaponProgress[w] = new WeaponProgress(lvl: 1);
        }
    }

    public void AddWeapon(string name)
    {
        WeaponData found = acquireableWeapons.Find(w => w.name == name);
        if (found == null) { Debug.Log("Weapon doesn't exist."); return; }

        if (weapons.Contains(found))
        {
            bool ok = TryUpgradeWeapon(found);
            Debug.Log(ok ? $"Upgraded {found.weaponName} to Lv.{GetWeaponLevel(found)}"
                              : $"Could not upgrade {found.weaponName} (level cap).");
            return;
        }

        weapons.Add(found);
        weaponProgress[found] = new WeaponProgress(lvl: 1);
    }

    private bool TryUpgradeWeapon(WeaponData weapon)
    {
        if (weapon == null) return false;
        if (!weaponProgress.TryGetValue(weapon, out var p)) return false;
        if (p.level >= weapon.maxLevel) return false;

        p.level++;
        weaponProgress[weapon] = p;

        // Unlock new skills if this level matches
        int index = weapon.skillUnlockLevels.IndexOf(p.level);
        if (index >= 0 && index < weapon.unlockableSkills.Count)
        {
            SkillData newSkill = weapon.unlockableSkills[index];
            if (!weapon.weaponSkills.Contains(newSkill))
            {
                weapon.weaponSkills.Add(newSkill);
                Debug.Log($"Unlocked new skill '{newSkill.skillName}' for {weapon.weaponName} at Lv.{p.level}!");
            }
        }

        return true;
    }

    public int GetWeaponLevel(WeaponData w)
    {
        if (w == null) return 0;
        return weaponProgress.TryGetValue(w, out var p) ? p.level : 0;
    }

    public int GetWeaponPower(WeaponData w)
    {
        if (w == null) return 0;
        if (!weaponProgress.TryGetValue(w, out var p)) return 0;
        // Linear growth example: base + perLevel*(level-1)
        return w.basePower + w.powerPerLevel * Mathf.Max(0, p.level - 1);
    }

    public void AddItem(string name)
    {
        foreach (var item in acquireableItems)
        {
            if (item.itemName == name)
            {
                items.Add(item);
                return;
            }
        }
        Debug.Log("Item doesn't exist.");
    }

    public void RemoveItem(ItemData item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }

    public void AddCoin(int number)
    {
        totalCoin += number;
    }

    private void ShowTab(string type)
    {
        inventoryFeaturesContainer.gameObject.SetActive(type == "Features");
        characterSelectionContainer.gameObject.SetActive(type == "CharacterSelectorWeapons" || type == "CharacterSelectorSkillTree");
        equippableWeaponsContainer.gameObject.SetActive(type == "EquippableWeapons");
        skillTreeContainer.gameObject.SetActive(type == "SkillTree");

        ClearAll();

        if (type == "Features")
        {
            previousType = "";
            inventoryFeaturesContainer.gameObject.SetActive(true);
        }

        if (type == "CharacterSelectorWeapons")
        {
            previousType = "Features";
            foreach (var character in GameController.Instance.playerCharacters)
            {
                var btn = Instantiate(characterSelectionPrefab, characterSelectionContainer);
                PlaceButtonNoises(btn);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = character.characterName;
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    currentSelectedCharacter = character;
                    ShowTab("EquippableWeapons");
                });
            }
        }

        if (type == "CharacterSelectorSkillTree")
        {
            previousType = "Features";
            foreach (var character in GameController.Instance.playerCharacters)
            {
                var btn = Instantiate(characterSelectionPrefab, characterSelectionContainer);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = character.characterName;

                PlaceButtonNoises(btn);
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Debug.Log($"[InventoryManager] Clicked character '{character.characterName}' -> open SkillTree");
                    currentSelectedCharacter = character;
                    ShowTab("SkillTree");
                });
            }
        }

        if (type == "EquippableWeapons")
        {
            previousType = "CharacterSelectorWeapons";
            foreach (var weapon in weapons)
            {
                if (currentSelectedCharacter.equipableWeaponTypes.Contains(weapon.weaponType))
                {
                    var btn = Instantiate(equippableWeaponPrefab, equippableWeaponsContainer);

                    int lvl = GetWeaponLevel(weapon);
                    btn.GetComponentInChildren<TextMeshProUGUI>().text = $"{weapon.weaponName} (Lv.{lvl})";
                    PlaceButtonNoises(btn);
    
                    var trigger = btn.GetComponent<WeaponTooltipTrigger>();
                    if (trigger == null) trigger = btn.gameObject.AddComponent<WeaponTooltipTrigger>();

                    var capturedWeapon = weapon;
                    trigger.Init(capturedWeapon);
                    var buttonComponent = btn.GetComponent<Button>();

                    if (currentSelectedCharacter.EquippedWeapon == capturedWeapon)
                    {
                        SelectButton(buttonComponent);
                    }

                    buttonComponent.onClick.AddListener(() =>
                    {
                        currentSelectedCharacter.EquippedWeapon = capturedWeapon;
                        SelectButton(buttonComponent);
                    });
                }
            }
        }

        if (type == "SkillTree")
        {
            previousType = "CharacterSelectorSkillTree";
            Debug.Log("[InventoryManager] Enter SkillTree tab");
            if (currentSelectedCharacter == null)
            {
                Debug.LogError("[InventoryManager] currentSelectedCharacter is NULL!");
                return;
            }

            var controller = skillTreeContainer.GetComponent<SkillTreeController>();
            if (controller == null)
            {
                Debug.LogError("[InventoryManager] SkillTreeController MISSING on skillTreeContainer object.");
                return;
            }

            Debug.Log($"[InventoryManager] Binding tree for '{currentSelectedCharacter.characterName}'");
            controller.Bind(currentSelectedCharacter);
            return;
        }

    }

    private void SelectButton(Button btn)
    {
        // Reset the previous buttons color
        if (currentlySelectedBtn != null)
        {
            EventSystem eventsystem = EventSystem.current;
            eventsystem.SetSelectedGameObject(null);
        }

        // Apply red to the newly selected button
        currentlySelectedBtn = btn;
        btn.Select();
    }

    private void ClearAll()
    {
        foreach (Transform child in characterSelectionContainer) Destroy(child.gameObject);
        foreach (Transform child in equippableWeaponsContainer) Destroy(child.gameObject);
    }

    public void previousMenu()
    {
        if (previousType == "")
        {
            featuresHUD.SetActive(false);
            GameController.Instance.cameraPan.PanTo(GameController.Instance.dialogueCamAnchor);
        }
        else
        {
            ShowTab(previousType);
        }
    }

    public void RevealInventoryButton()
    {
        revealedInventory = true;
        inventoryButton.gameObject.SetActive(true);
    }

    private void PlaceButtonNoises(GameObject button)
    {
        button.gameObject.AddComponent<ButtonNoise>();
        var grabbedButtonNoise = button.GetComponentInChildren<ButtonNoise>();
        grabbedButtonNoise.AddAudioSource(audioSource);
        grabbedButtonNoise.AddHoverClip(hoverClip);
        grabbedButtonNoise.AddPressedClip(pressedClip);
        grabbedButtonNoise.AssignSelf(button.GetComponentInChildren<Button>());
    }
}
