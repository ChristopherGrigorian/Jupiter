using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatHudManager : MonoBehaviour
{
    [Header("Containers")]
    public Transform weaponContainer;
    public Transform skillContainer;
    public Transform itemContainer;

    [Header("Prefabs")]
    public GameObject weaponButtonPrefab;
    public GameObject skillButtonPrefab;
    public GameObject itemButtonPrefab;

    public Button weaponTabButton;
    public Button skillTabButton;
    public Button itemTabButton;
   

    private CharacterData activeCharacter;

    private void Start()
    {
        weaponTabButton.onClick.AddListener(() => ShowTab("Weapon"));
        skillTabButton.onClick.AddListener(() => ShowTab("Skill"));
        itemTabButton.onClick.AddListener(() => ShowTab("Item"));
    }

    public void PopulateActions(CharacterData character, System.Action<SkillData> onSkillChosen)
    {
        activeCharacter = character;

        // Store callback for use in ShowTab
        cachedSkillCallback = onSkillChosen;

        ShowTab("Weapon"); // default view
    }

    private System.Action<SkillData> cachedSkillCallback;

    private void ShowTab(string type)
    {
        weaponContainer.gameObject.SetActive(type == "Weapon");
        skillContainer.gameObject.SetActive(type == "Skill");
        itemContainer.gameObject.SetActive(type == "Item");

        ClearAll();

        if (type == "Weapon" && activeCharacter.EquippedWeapon != null)
        {
            foreach (var skill in activeCharacter.EquippedWeapon.weaponSkills)
            {
                var btn = Instantiate(weaponButtonPrefab, weaponContainer);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = skill.skillName;

                var trigger = btn.AddComponent<SkillTooltipTrigger>();
                trigger.Init(skill);

                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    cachedSkillCallback?.Invoke(skill);
                });
            }
        }

        if (type == "Skill")
        {
            foreach (var skill in activeCharacter.EquippedSkills)
            {
                var btn = Instantiate(skillButtonPrefab, skillContainer);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = skill.skillName;

                var trigger = btn.AddComponent<SkillTooltipTrigger>();
                trigger.Init(skill);

                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    cachedSkillCallback?.Invoke(skill);
                });
            }
        }


        // Optional: item support
    }

    private void ClearAll()
    {
        foreach (Transform child in weaponContainer) Destroy(child.gameObject);
        foreach (Transform child in skillContainer) Destroy(child.gameObject);
        foreach (Transform child in itemContainer) Destroy(child.gameObject);
    }

    public void SetAllActionButtonsInteractable(bool interactable)
    {
        SetButtonsInContainer(weaponContainer, interactable);
        SetButtonsInContainer(skillContainer, interactable);
        SetButtonsInContainer(itemContainer, interactable);
    }

    private void SetButtonsInContainer(Transform container, bool interactable)
    {
        foreach (Transform child in container)
        {
            var button = child.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = interactable;
            }
        }
    }

}


