using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Saves/SaveData")]
public class SaveData : ScriptableObject
{

    public List<WeaponData> weapons;
    public Dictionary<WeaponData, WeaponProgress> weaponProgress;
    public List<ItemData> items;
    public List<CharacterData> playerCharacters;

    [Header("Ink snapshot (serialized in this SO)")]
    [TextArea(3, 30)]
    [SerializeField] private string inkStoryJson = "";   // holds Story.state JSON
    private string currentStoryText = "";

    public bool storyFlippedInventory = false;
    public bool storyFlippedMap = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SaveGame()
    {
        weapons = InventoryManager.Instance.weapons;
        weaponProgress = InventoryManager.Instance.weaponProgress;
        items = InventoryManager.Instance.items;
        playerCharacters = GameController.Instance.playerCharacters;
        currentStoryText = InkDialogueManager.Instance.heldStory;

        storyFlippedInventory = InventoryManager.Instance.revealedInvenetory;
        storyFlippedMap = MapManager.Instance.revealedMap;

        SaveInkStory();
        Application.Quit();
    }

    public void LoadGame()
    {
        InventoryManager.Instance.weapons = weapons;
        InventoryManager.Instance.weaponProgress = weaponProgress;
        InventoryManager.Instance.items = items;
        GameController.Instance.playerCharacters = playerCharacters;
        InkDialogueManager.Instance.RestoreDialogueText(currentStoryText);
        
        if (storyFlippedMap) MapManager.Instance.RevealMapButton();
        if (storyFlippedInventory) InventoryManager.Instance.RevealInventoryButton();
        LoadInkStory();
    }

    private void SaveInkStory()
    {
        if (InkDialogueManager.Instance.story == null) return;
        inkStoryJson = InkDialogueManager.Instance.story.state.ToJson();
    }

    private void LoadInkStory()
    {
        var dm = InkDialogueManager.Instance;
        if (dm == null || dm.story == null) return;

        // 1) Load saved VM state FIRST
        if (!string.IsNullOrEmpty(inkStoryJson))
            dm.story.state.LoadJson(inkStoryJson);

        // 2) Now show the panel (and only advance if you want)
        dm.ShowDialoguePanelFromTitleLoad();
        // Optional: if you want to advance immediately after load:
        // if (dm.story.canContinue) dm.locationChange(GetCurrentKnot(dm.story)); // or dm.ContinueStory()
    }


}
