using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Saves/SaveData")]
public class SaveData : ScriptableObject
{
    // ======== PUBLIC ENTRY POINTS (call these like before) ========

    public void SaveGame()
    {
        var snap = BuildSnapshotFromLiveGame();
        WriteSnapshotToDisk(snap);

#if UNITY_EDITOR
        Debug.Log($"[SaveData] Saved → {SavePath}");
#endif
        Application.Quit();
    }

    public void LoadGame()
    {
        if (!TryReadSnapshot(out var snap))
        {
#if UNITY_EDITOR
            Debug.LogWarning("[SaveData] No save file found. Skipping Load.");
#endif
            return;
        }

        ApplySnapshotToLiveGame(snap);
#if UNITY_EDITOR
        Debug.Log($"[SaveData] Loaded ← {SavePath}");
#endif
    }

    // Optional helpers you can call from menus/buttons:
    public bool SaveExists() => File.Exists(SavePath);
    public void DeleteSave()
    {
        if (File.Exists(SavePath)) File.Delete(SavePath);
    }

    // ======== JSON-FRIENDLY SNAPSHOT TYPES (no Unity refs, no Dictionary) ========

    [Serializable]
    private class Snapshot
    {
        public List<string> weapons = new();                    // WeaponData IDs
        public List<WeaponProgressRecord> progress = new();     // (weaponId, data)
        public List<string> items = new();                      // ItemData IDs
        public List<string> playerCharacters = new();           // CharacterData IDs

        public string inkStoryJson = "";
        public string currentStoryText = "";

        public bool storyFlippedInventory = false;
        public bool storyFlippedMap = false;
    }

    [Serializable]
    private class WeaponProgressRecord
    {
        public string weaponId;
        public int level;
        public float xp;

        // If your WeaponProgress has more fields, add them here (e.g. perks, upgrades, etc.)
    }

    // ======== BUILD SNAPSHOT FROM LIVE GAME (read your singletons, map to IDs) ========

    private Snapshot BuildSnapshotFromLiveGame()
    {
        var snap = new Snapshot();

        // Weapons → IDs
        foreach (var w in InventoryManager.Instance.weapons)
            if (w != null) snap.weapons.Add(w.id);

        // Weapon progress → list (no Dictionary in JSON)
        foreach (var kv in InventoryManager.Instance.weaponProgress)
        {
            if (kv.Key == null) continue;
            snap.progress.Add(new WeaponProgressRecord
            {
                weaponId = kv.Key.id,
                level = kv.Value.level,
                
            });
        }

        // Items → IDs
        foreach (var it in InventoryManager.Instance.items)
            if (it != null) snap.items.Add(it.id);

        // Party → IDs
        foreach (var ch in GameController.Instance.playerCharacters)
            if (ch != null) snap.playerCharacters.Add(ch.id);

        // Ink + UI flags
        snap.currentStoryText = InkDialogueManager.Instance?.heldStory ?? "";
        snap.inkStoryJson = InkDialogueManager.Instance?.story != null
            ? InkDialogueManager.Instance.story.state.ToJson()
            : "";

        snap.storyFlippedInventory = InventoryManager.Instance.revealedInventory;
        snap.storyFlippedMap = MapManager.Instance.revealedMap;

        return snap;
    }

    // ======== APPLY SNAPSHOT BACK TO GAME (IDs → assets via a DB in Resources) ========

    private void ApplySnapshotToLiveGame(Snapshot snap)
    {
        // Expect a small DB ScriptableObject under Resources/ named "GameDB"
        // with lists of all WeaponData, ItemData, CharacterData and simple find-by-id helpers.
        var db = Resources.Load<GameDB>("GameDB");
        if (!db)
        {
            Debug.LogError("[SaveData] GameDB not found in Resources/. Create one and name it 'GameDB'.");
            return;
        }

        // Weapons
        InventoryManager.Instance.weapons = new List<WeaponData>();
        foreach (var id in snap.weapons)
        {
            var w = db.WeaponById(id);
            if (w) InventoryManager.Instance.weapons.Add(w);
        }

        // Weapon progress
        InventoryManager.Instance.weaponProgress = new Dictionary<WeaponData, WeaponProgress>();
        foreach (var rec in snap.progress)
        {
            var w = db.WeaponById(rec.weaponId);
            if (!w) continue;
            var wp = new WeaponProgress { level = rec.level };
            InventoryManager.Instance.weaponProgress[w] = wp;
        }

        // Items
        InventoryManager.Instance.items = new List<ItemData>();
        foreach (var id in snap.items)
        {
            var it = db.ItemById(id);
            if (it) InventoryManager.Instance.items.Add(it);
        }

        // Party
        GameController.Instance.playerCharacters = new List<CharacterData>();
        foreach (var id in snap.playerCharacters)
        {
            var ch = db.CharById(id);
            if (ch) GameController.Instance.playerCharacters.Add(ch);
        }

        // UI flags
        if (snap.storyFlippedMap) MapManager.Instance.RevealMapButton();
        if (snap.storyFlippedInventory) InventoryManager.Instance.RevealInventoryButton();

        // Ink
        var dm = InkDialogueManager.Instance;
        if (dm?.story != null && !string.IsNullOrEmpty(snap.inkStoryJson))
            dm.story.state.LoadJson(snap.inkStoryJson);

        dm?.RestoreDialogueText(snap.currentStoryText);
        dm?.ShowDialoguePanelFromTitleLoad();
    }

    // ======== DISK I/O (build-safe) ========

    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, "save.json");

    private static void WriteSnapshotToDisk(Snapshot snap)
    {
        var json = JsonUtility.ToJson(snap, false);
        File.WriteAllText(SavePath, json);
    }

    private static bool TryReadSnapshot(out Snapshot snap)
    {
        if (!File.Exists(SavePath))
        {
            snap = null;
            return false;
        }
        var json = File.ReadAllText(SavePath);
        snap = JsonUtility.FromJson<Snapshot>(json);
        return snap != null;
    }
}
