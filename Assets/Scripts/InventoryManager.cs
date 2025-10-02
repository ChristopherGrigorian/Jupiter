using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public List<WeaponData> weapons;

    [SerializeField] private List<WeaponData> acquireableWeapons;

    [SerializeField] public List<ItemData> items;
    [SerializeField] private List<ItemData> acquireableItems;

    [SerializeField] private GameObject toolTipHolder;

    public int totalCoin = 0;

    private Button currentlySelectedBtn;

    [Header("Containers")]
    public Transform inventoryFeaturesContainer;
    public Transform characterSelectionContainer;
    public Transform equippableWeaponsContainer;
    public Transform skillTreeContainer;
    public Transform teamSelectContainer;

    [Header("Prefabs")]
    public GameObject characterSelectionPrefab;
    public GameObject equippableWeaponPrefab;
    public GameObject teamSelectionPrefab;

    [Header("Pre-Established Inventory Features / Buttons")]
    public Button inventoryButton;
    public Button weaponInventoryButton;
    public Button skillTreeButton;
    public Button teamSelectButton;
    public Button characterStatsButton;

    [Header("Character Stats Menu")]
    public GameObject characterStatsHUD;
    public Image charImageRed;
    public Image charImageBlue;
    public Image charImage;
    public Image expSprite;
    public TextMeshProUGUI currentWeapon;
    public TextMeshProUGUI characterNameBox;
    public TextMeshProUGUI maxCharHealth;
    public TextMeshProUGUI maxCharMP;
    public TextMeshProUGUI charStrength;
    public TextMeshProUGUI charPerception;
    public TextMeshProUGUI charEvas;
    public TextMeshProUGUI charSpirit;
    public TextMeshProUGUI charSpeed;
    public TextMeshProUGUI level;

    [Header("Button Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip pressedClip;

    private string previousType = "";

    public Dictionary<WeaponData, WeaponProgress> weaponProgress = new();

    public bool revealedInventory = false;

    [SerializeField] private GameObject featuresHUD;
    [SerializeField] private GameObject dialogueHUD;
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
        teamSelectButton.onClick.AddListener(() => ShowTab("TeamSelector"));
        characterStatsButton.onClick.AddListener(() => ShowTab("CharacterStatsCS"));

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

    private void ToggleHUD(GameObject hud, bool show)
    {
        if (!hud) return;
        var fader = hud.GetComponent<UIFader>();
        if (fader == null)
        {
            Debug.Log("the fader was null");
            // fallback, but strongly recommend adding UIFader to all HUDs
            hud.SetActive(show);
            return;
        }

        if (show) fader.Show();
        else if (hud.activeInHierarchy) fader.Hide();
    }

    private void ShowTab(string type)
    {
        inventoryFeaturesContainer.gameObject.SetActive(type == "Features");
        characterSelectionContainer.gameObject.SetActive(type == "CharacterSelectorWeapons" || type == "CharacterSelectorSkillTree" || type == "CharacterStatsCS");
        equippableWeaponsContainer.gameObject.SetActive(type == "EquippableWeapons");
        skillTreeContainer.gameObject.SetActive(type == "SkillTree");
        teamSelectContainer.gameObject.SetActive(type == "TeamSelector");
        ToggleHUD(characterStatsHUD, type == "CharacterStats");

        ClearAll();

        if (type == "Features")
        {
            toolTipHolder.gameObject.SetActive(false);
            previousType = "";

            ToggleHUD(dialogueHUD, false);
            ToggleHUD(featuresHUD, true);  
        }

        if (type == "CharacterSelectorWeapons")
        {
            toolTipHolder.gameObject.SetActive(true);
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
            toolTipHolder.gameObject.SetActive(true);
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

        if (type == "CharacterStatsCS")
        {
            toolTipHolder.gameObject.SetActive(true);
            previousType = "Features";
            foreach (var character in GameController.Instance.playerCharacters)
            {
                var btn = Instantiate(characterSelectionPrefab, characterSelectionContainer);
                PlaceButtonNoises(btn);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = character.characterName;
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    currentSelectedCharacter = character;
                    ShowTab("CharacterStats");
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

        if (type == "TeamSelector")
        {
            toolTipHolder.gameObject.SetActive(true);
            previousType = "Features";

            foreach (var character in GameController.Instance.playerCharacters)
            {
                var btn = Instantiate(teamSelectionPrefab, teamSelectContainer);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = $"{character.characterName}";
                PlaceButtonNoises(btn);
               
                var buttonComponent = btn.GetComponent<Button>();

                if (character.currentlyEquipped)
                    btn.GetComponentInChildren<Image>().color = Color.red;

                buttonComponent.onClick.AddListener(() =>
                {
                    int equippedCount = GameController.Instance.playerCharacters.Count(c => c.currentlyEquipped);

                    // If this character is not equipped yet and we already have 3, block
                    if (!character.currentlyEquipped && equippedCount >= 3)
                        return;

                    character.currentlyEquipped = !character.currentlyEquipped;

                    if (character.currentlyEquipped)
                    {
                        btn.GetComponentInChildren<Image>().color = Color.red;
                    } else
                    {
                        btn.GetComponentInChildren<Image>().color = Color.white;
                    }
                });
            }
        }

        if (type == "CharacterStats")
        {
            float fillAmount = currentSelectedCharacter.XPNeededToNextLevel() / (float) currentSelectedCharacter.GetRequiredXPForLevel(currentSelectedCharacter.level + 1);
            expSprite.fillAmount = fillAmount;
            characterNameBox.text = currentSelectedCharacter.characterName;
            maxCharHealth.text = $"Max Health:\n" + currentSelectedCharacter.maxHP.ToString();
            maxCharMP.text = $"Max MP:\n" + currentSelectedCharacter.maxMP.ToString();
            charStrength.text = $"Strength: " + currentSelectedCharacter.strength.ToString();
            charPerception.text = $"Perception: " + currentSelectedCharacter.perception.ToString();
            charEvas.text = $"Evasion: " + currentSelectedCharacter.evasiveness.ToString();
            charSpirit.text = $"Spirit: " + currentSelectedCharacter.spirit.ToString();
            charSpeed.text = $"Speed:" + currentSelectedCharacter.speed.ToString();
            level.text = $"Level " + currentSelectedCharacter.level.ToString();
            currentWeapon.text = $"Current Weapon:\n" + currentSelectedCharacter.EquippedWeapon.weaponName;

            CharacterFlipBook(currentSelectedCharacter);
        }
    }

    private Coroutine flipbookCoroutine;

    private void CharacterFlipBook(CharacterData combatant)
    {
        if (flipbookCoroutine != null)
            StopCoroutine(flipbookCoroutine);

        flipbookCoroutine = StartCoroutine(FlipbookRoutine(combatant.Images));
    }

    private IEnumerator FlipbookRoutine(List<Sprite> images)
    {
        int index = 0;
        while (true)
        {
            if (images == null || images.Count == 0)
            {
                charImage.sprite = null;
                charImageRed.sprite = null;
                charImageBlue.sprite = null;
                yield break;
            }

            charImage.sprite = images[index];
            charImageRed.sprite = images[(index + 1) % images.Count];
            charImageBlue.sprite = images[(index + 2) % images.Count];
            index = (index + 1) % images.Count;

            yield return new WaitForSeconds(0.2f);
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
        foreach (Transform child in teamSelectContainer) Destroy (child.gameObject);
    }

    public void previousMenu()
    {
        if (previousType == "")
        {
            ToggleHUD(featuresHUD, false);
            ToggleHUD(dialogueHUD, true);
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
