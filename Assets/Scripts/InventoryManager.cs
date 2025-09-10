using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private string previousType = "";


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
    }

    public void AddWeapon(string name) 
    {
        foreach (var weapon in acquireableWeapons) 
        {
            if (weapon.name == name)
            {
                weapons.Add(weapon);
                return;
            }
        }

        Debug.Log("Weapon doesn't exist.");
    }

    public void AddItem(string name)
    {
        foreach (var item in acquireableItems)
        {
            if (item.name == name)
            {
                items.Add(item);
                return;
            }
        }
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
            foreach(var weapon in weapons)
            {
                if (currentSelectedCharacter.equipableWeaponTypes.Contains(weapon.weaponType))
                {
                    var btn = Instantiate(equippableWeaponPrefab, equippableWeaponsContainer);
                    btn.GetComponentInChildren<TextMeshProUGUI>().text = weapon.weaponName;

                    var capturedWeapon = weapon;
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
        // Reset the previous button�s color
        if (currentlySelectedBtn != null)
            currentlySelectedBtn.GetComponent<Image>().color = defaultColor;

        // Apply red to the newly selected button
        currentlySelectedBtn = btn;
        currentlySelectedBtn.GetComponent<Image>().color = selectedColor;
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
}
