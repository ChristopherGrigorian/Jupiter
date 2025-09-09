using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class GameController : MonoBehaviour
{

    public List<CharacterData> allCharacters;
    public List<CharacterData> playerCharacters;
    public List<CharacterData> enemyCharacters;
    public InkDialogueManager dialogueManager;

    private List<Combatant> turnOrder = new();
    private int currentTurnIndex = 0;
    private bool combatActive = false;

    [SerializeField] private List<EncounterData> allEncounters;
    [SerializeField] private CombatHudManager combatHudManager;

    [SerializeField] private GameObject combatHUD;
    [SerializeField] private GameObject dialogueHUD;
    [SerializeField] private Transform targetContainer;

    [SerializeField] private GameObject combatantButtonPrefab;

    [SerializeField] private Image characterImage;

    [SerializeField] private GameObject combatLogPanel;
    [SerializeField] private TextMeshProUGUI combatLogText;

    [SerializeField] private GameObject weaponBacking;
    [SerializeField] private GameObject actionContainer;

    [Header("CombatantInformation")]
    [SerializeField] private TextMeshProUGUI CombatantName;
    [SerializeField] private TextMeshProUGUI CombatantHealth;
    [SerializeField] private TextMeshProUGUI CombatantMP;

    // GameController fields
    [Header("Camera")]
    [SerializeField] public CameraPan cameraPan;
    [SerializeField] public Transform dialogueCamAnchor;
    [SerializeField] private Transform combatCamAnchor;

    [SerializeField] private Transform allyTargetCamAnchor;
    [SerializeField] private Transform enemyTargetCamAnchor;

    [Header("Targeting")]
    [SerializeField] private LayerMask pedestalMask;

    [Header("Pedestals")]
    [SerializeField] private GameObject pedestalPrefab;
    [SerializeField] private Transform playerRowAnchor;
    [SerializeField] private Transform enemyRowAnchor;
    [SerializeField] private float rowSpacing = 6.0f; // distance between pedestals

    private readonly Dictionary<Combatant, PedestalController> pedestalMap = new();

    [Header("LevelUpItems")]
    [SerializeField] private LevelUpPanelController levelUpPanel;
    private List<LevelUpSummary> _lastVictorySummaries; // built inside CheckVictoryCondition

    [Header("Audio")]
    [SerializeField] public AudioSource sfxSource;

    private Dictionary<string, EncounterData> encounterMap;

    private Combatant target;

    private string pendingContinueKnot = "";

    private List<Combatant> defeatedEnemiesInCurrentEncounter;

    public static GameController Instance;

    private bool selectionCanceled = false;


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

        encounterMap = new Dictionary<string, EncounterData>();
        foreach (var encounter in allEncounters)
        {
            encounterMap[encounter.encounterID] = encounter;
        }
    }


    public void StartCombat(string encounterID, string continueKnot)
    {
        if (!encounterMap.ContainsKey(encounterID))
        {
            Debug.LogError($"No encounter found with ID: {encounterID}");
            return;
        }

        if (cameraPan != null && combatCamAnchor != null)
            cameraPan.PanTo(combatCamAnchor);

        combatHUD.SetActive(true);
        dialogueHUD.SetActive(false);

        combatActive = true;
        var encounter = encounterMap[encounterID];
        enemyCharacters = encounter.enemies;

        pendingContinueKnot = continueKnot;

        dialogueManager.SetCombatState(true, null);
        PrepareCombatants();

        SpawnAllPedestals();

        currentTurnIndex = 0;

        StartCoroutine(CombatLoop());
    }

    private void PrepareCombatants()
    {
        turnOrder.Clear();

        foreach (var p in playerCharacters)
        {
            turnOrder.Add(new Combatant(p, true));
        }
        foreach (var e in enemyCharacters)
        {
            turnOrder.Add(new Combatant(e, false));
        }

        turnOrder.Sort((a, b) => b.data.speed.CompareTo(a.data.speed));
    }

    private IEnumerator CombatLoop()
    {
        Debug.Log("EnteredCombatLoop");
        while (combatActive) {
            Combatant current = turnOrder[currentTurnIndex];

            if (current.currentHP > 0)
            {
                Debug.Log($"It is now {current.Name}'s turn.");

                CombatantName.text = "Current Combatant: " + current.Name;
                CombatantHealth.text = "Combatant HP: " + current.currentHP.ToString();
                CombatantMP.text = "Combatant MP: " + current.currentMP.ToString();

                CharacterFlipBook(current);
                yield return current.isPlayerControlled
                    ? StartCoroutine(PlayerTurn(current))
                    : StartCoroutine(EnemyTurn(current));
            }

            currentTurnIndex = (currentTurnIndex + 1) % turnOrder.Count;

            if (CheckVictoryCondition())
            {
                yield return StartCoroutine(AwardWeapons(defeatedEnemiesInCurrentEncounter));
                yield return StartCoroutine(AwardCoin(defeatedEnemiesInCurrentEncounter));

                if (levelUpPanel != null && _lastVictorySummaries != null && _lastVictorySummaries.Count > 0)
                    Debug.Log("I saw this");
                    yield return StartCoroutine(levelUpPanel.ShowSequence(_lastVictorySummaries));

                if (cameraPan != null && dialogueCamAnchor != null)
                    cameraPan.PanTo(dialogueCamAnchor);

                combatHUD.SetActive(false);
                dialogueHUD.SetActive(true);

                defeatedEnemiesInCurrentEncounter.Clear();
                ClearAllPedestals();
                break;
            }

            yield return null;
        }

        dialogueManager.SetCombatState(false, pendingContinueKnot);
    }

    private IEnumerator PlayerTurn(Combatant player)
    {
        while (true)
        {
            combatHudManager.SetAllActionButtonsInteractable(true);
            weaponBacking.SetActive(true);
            actionContainer.SetActive(true);
            Debug.Log($"Player {player.Name}'s turn. Choose a skill.");

            SkillData chosenSkill = null;

            // Populate the action UI and define what happens on click
            combatHudManager.PopulateActions(player.data, (SkillData skill) =>
            {

                if (!CheckMPReqMet(player, skill))
                {
                    Debug.LogWarning("Not enough MP! Choose a different skill.");
                    return; // Don't end the turn, just ignore this selection
                }

                Debug.Log($"{player.Name} uses {skill.skillName}!");
                chosenSkill = skill;
                combatHudManager.SetAllActionButtonsInteractable(false);
            });

            yield return new WaitUntil(() => chosenSkill != null);
            //combatHUD.SetActive(false);
            yield return StartCoroutine(TargetSelection(player, chosenSkill));
            //combatHUD.SetActive(true);

            if (selectionCanceled || target == null)
            {
                combatHudManager.SetAllActionButtonsInteractable(true);
                chosenSkill = null;
                continue;
            }

            SpendMP(player, chosenSkill);
            yield return StartCoroutine(PlayerAction(player, chosenSkill));
            player.currentMP += 1;

            yield return StartCoroutine(ResolveEndOfTurnStatuses(player));
            yield break;
        }
    }

    private IEnumerator PlayerAction(Combatant player, SkillData chosenSkill)
    {
        switch (chosenSkill.type)
        {
            case SkillType.Attack:
                int damage = chosenSkill.power;
                if (RollToHit(player.EffectivePerception, target.EffectiveEvasiveness))
                {
                    PlaySkillSfx(chosenSkill);
                    if (RollToCrit(player.EffectiveSpirit))
                    {
                        StartCoroutine(ShowCombatLog($"{player}'s attack was a critical hit!"));
                        damage *= 2;
                    }
                    int totalDamage = damage + (player.EffectiveStrength / 2);
                    target.currentHP = Mathf.Max(0, target.currentHP - totalDamage);
                    RefreshUIFor(target);
                    yield return StartCoroutine(ShowCombatLog($"{target.Name} takes {totalDamage} damage!"));
                }
                else
                {
                    sfxSource.PlayOneShot(target.data.dodgeSound);
                    yield return StartCoroutine(ShowCombatLog($"{target.Name} dodges the attack!"));
                }
                break;
            case SkillType.Heal:
                PlaySkillSfx(chosenSkill);
                int heal = chosenSkill.potency;
                target.currentHP += heal;
                RefreshUIFor(target);
                yield return StartCoroutine(ShowCombatLog($"{player.Name} healed for {heal}."));
                break;
            case SkillType.Buff:
                PlaySkillSfx(chosenSkill);
                List<StatusToApply> statusEffect = chosenSkill.statusesToApply;
                foreach (var s in statusEffect)
                {
                    for (int i = 0; i < s.status.statModifiers.Length; i++)
                    {
                        var mod = s.status.statModifiers[i];
                        int spiritBonus = player.EffectiveSpirit / 2;
                        mod.flatDelta = chosenSkill.potency;
                        mod.effectiveFlatDelta = mod.flatDelta + spiritBonus;
                        s.status.statModifiers[i] = mod;
                    }
                }
                RefreshUIFor(target);
                yield return StartCoroutine(ShowCombatLog($"{player.Name} utilized {chosenSkill.name}"));
                break;
            default:
                Debug.Log("That wasn't an attack ability.");
                break;
        }
        yield return StartCoroutine(TryApplyStatuses(player, target, chosenSkill));
    }

    public IEnumerator TryApplyStatuses(Combatant source, Combatant target, SkillData skill)
    {
        if (skill.statusesToApply == null) yield break;

        foreach (var s in skill.statusesToApply)
        {
            if (s.status == null) continue;
            if (UnityEngine.Random.value <= s.chance)
            {
                target.ApplyStatus(s.status, s.durationOverride, Mathf.Max(1, s.stacks));
                yield return StartCoroutine(ShowCombatLog($"{target.Name} is afflicted with {s.status.displayName}!"));
            }
        }
    }

    private IEnumerator ResolveEndOfTurnStatuses(Combatant acting)
    {
        if (acting == null || !acting.IsAlive) yield break;

        var logs = new List<string>();

        foreach (var s in acting.statuses.ToList())
        {
            if (s.remainingTurns <= 0) continue;

            int dot = s.data.damagePerTurn * Mathf.Max(1, s.stacks);
            if (dot > 0)
            {
                acting.currentHP = Mathf.Max(0, acting.currentHP - dot);
                logs.Add($"{acting.Name} suffers {dot} damage from {s.data.displayName}.");
                RefreshUIFor(acting);
            }

            s.remainingTurns--;
            if (s.remainingTurns <= 0)
                logs.Add($"{acting.Name} is no longer {s.data.displayName}.");
        }

        foreach (var msg in logs)
            yield return StartCoroutine(ShowCombatLog(msg));

        RefreshUIFor(acting);
    }


    public bool RollToHit(int attackerPerception, int defenderEvasiveness)
    {
        float hitChance = CalculateHitChance(attackerPerception, defenderEvasiveness);

        float roll = UnityEngine.Random.value; // Random float between 0.0 and 1.0

        return roll <= hitChance;
    }

    public bool RollToCrit(int spirit)
    {
        float critChance = CalculateCritChance(spirit);
        float critRoll = UnityEngine.Random.value;
        return critRoll <= critChance;
    }

    float CalculateCritChance(int spirit)
    {
        // Adjust these values to fit your balance
        float maxCritChance = 0.5f; // cap at 50%
        float curveFactor = 25f;    // controls steepness

        // Logistic-like curve: higher spirit gives more crit, but never reaches 100%
        return Mathf.Clamp01(spirit / (spirit + curveFactor)) * maxCritChance;
    }


    private float CalculateHitChance(int perception, int evasiveness)
    {
        float minHitChance = 0.05f; // 5%
        float maxHitChance = 0.95f; // 95%

        float perc = Mathf.Max(1, perception);
        float evas = Mathf.Max(1, evasiveness);

        // Ratio between perception and total (with a small bias to avoid div-by-zero)
        float ratio = perc / (perc + evas + 1f); // +1 to avoid 0/0

        // Clamp final hit chance between min and max
        return Mathf.Clamp(ratio, minHitChance, maxHitChance);
    }


    private bool CheckMPReqMet(Combatant combatant, SkillData chosenSkill)
    {
        if (combatant.currentMP >= chosenSkill.MPCost)
        {
            return true;
        }
        Debug.Log("Not enough MP for action");
        return false;
    }

    private void SpendMP(Combatant combatant, SkillData chosenSkill)
    {
        combatant.currentMP -= chosenSkill.MPCost;
    }

    private IEnumerator EnemyTurn(Combatant enemy)
    {

        combatHudManager.SetAllActionButtonsInteractable(false);
        weaponBacking.SetActive(false);
        actionContainer.SetActive(false);
        yield return new WaitForSeconds(1f);

        var ai = new EnemyAI(enemy, turnOrder);
        yield return StartCoroutine(ai.Run());
        yield return StartCoroutine(ResolveEndOfTurnStatuses(enemy));
        yield return null;
    }

    private bool CheckVictoryCondition()
    {
        bool playersAlive = turnOrder.Exists(t => t.isPlayerControlled && t.IsAlive);
        bool enemiesAlive = turnOrder.Exists(t => !t.isPlayerControlled && t.IsAlive);

        if (!playersAlive || !enemiesAlive)
        {
            combatActive = false;
            Debug.Log(playersAlive ? "Victory!" : "Defeat...");
            if (playersAlive)
            {
                List<Combatant> defeatedEnemies = turnOrder.Where(e => !e.IsAlive && !e.isPlayerControlled).ToList();
                List<Combatant> survivingPlayers = turnOrder.Where(p => p.IsAlive && p.isPlayerControlled).ToList();

                defeatedEnemiesInCurrentEncounter = defeatedEnemies;

                var beforeMap = new Dictionary<CharacterData, CharacterStatsSnapshot>();
                foreach (var s in survivingPlayers)
                    beforeMap[s.data] = CharacterStatsSnapshot.Capture(s.data);

                int xpPerMember = XPUtility.AwardXPToParty(defeatedEnemies, survivingPlayers);

                _lastVictorySummaries = BuildLevelUpSummaries(survivingPlayers, beforeMap, xpPerMember);
                
            }


            return true;
        }

        return false;

    }

    private IEnumerator AwardWeapons(List<Combatant> defeatedEnemies)
    {
        foreach (var enemy in defeatedEnemies)
        {
            if (enemy.data.droppableWeapons.Count > 0)
            {
                foreach (var weapon in enemy.data.droppableWeapons)
                {
                    InventoryManager.Instance.AddWeapon(weapon.name);
                    yield return StartCoroutine(ShowCombatLog($"You obtain {weapon.name}."));
                }
            }
        }
    }

    private IEnumerator AwardCoin(List<Combatant> defeatedEnemies)
    {
        int totalCoinEarned = 0;
        foreach(var enemy in defeatedEnemies)
        {
            int awardedCoin = enemy.data.heldCoin;
            if (enemy.data.heldCoin > 0)
            {
                InventoryManager.Instance.totalCoin += awardedCoin;
                totalCoinEarned += awardedCoin;
            }
        }
        yield return StartCoroutine(ShowCombatLog($"You looted {totalCoinEarned} coins from this fight."));
    }

    private IEnumerator TargetSelection(Combatant player, SkillData skill)
    {
        target = null;

        if (cameraPan != null)
        {
            if (skill.targetsEnemies && enemyTargetCamAnchor)
                cameraPan.PanTo(enemyTargetCamAnchor);
            else if (!skill.targetsEnemies && allyTargetCamAnchor)
                cameraPan.PanTo(allyTargetCamAnchor);
        }

        PedestalController lastHover = null;

        while (target == null)
        {
            // (Optional) ignore when mouse over UI
            // if (UnityEngine.EventSystems.EventSystem.current?.IsPointerOverGameObject() == true)
            // {
            //     SetHover(lastHover, false);
            //     lastHover = null;
            //     yield return null;
            //     continue;
            // }

            // Raycast from camera to mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, pedestalMask))
            {
                var ped = hit.collider.GetComponentInParent<PedestalController>();
                bool isValid = ped != null
                               && ped.Bound != null
                               && ped.Bound.IsAlive
                               && (skill.targetsEnemies ? !ped.Bound.isPlayerControlled : ped.Bound.isPlayerControlled);

                // Update hover highlight
                if (ped != lastHover)
                {
                    SetHover(lastHover, false);
                    lastHover = null;

                    if (isValid)
                    {
                        SetHover(ped, true);
                        lastHover = ped;
                    }
                }

                // Left click to confirm
                if (isValid && Input.GetMouseButtonDown(0))
                {
                    Debug.Log("I saw this");
                    target = ped.Bound;
                    selectionCanceled = false;
                    break;
                }
            }
            else
            {
                SetHover(lastHover, false);
                lastHover = null;
            }

            // Optional cancel (RMB / Esc) to abort targeting
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                // You could decide to break and consume the turn, or return to action menu.
                // Here we just cancel selection and return to action menu:
                selectionCanceled = true;
                break;
            }

            yield return null;
        }

        // Clear hover visuals after selection
        cameraPan.PanTo(combatCamAnchor);
        SetHover(lastHover, false);
        lastHover = null;
    }

    private void SetHover(PedestalController ped, bool on)
    {
        if (ped) ped.SetHover(on);
    }


    private void AllySelect()
    {
        foreach (var combatant in turnOrder)
        {
            if (combatant.isPlayerControlled && combatant.IsAlive) 
            {
                var btn = Instantiate(combatantButtonPrefab, targetContainer);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = combatant.data.characterName;
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    target = combatant;      
                });
            }
        }
    }

    private void EnemySelect()
    {
        foreach (var combatant in turnOrder)
        {
            if (!combatant.isPlayerControlled && combatant.IsAlive)
            {
                var btn = Instantiate(combatantButtonPrefab, targetContainer);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = combatant.data.characterName;
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    target = combatant;
                });
            }
        }
    }


    private void ClearTargetSelection()
    {
        foreach (Transform child in targetContainer) Destroy(child.gameObject);
    }


    private Coroutine flipbookCoroutine;

    private void CharacterFlipBook(Combatant combatant)
    {
        if (flipbookCoroutine != null)
            StopCoroutine(flipbookCoroutine);

        flipbookCoroutine = StartCoroutine(FlipbookRoutine(combatant.data.Images));
    }

    private IEnumerator FlipbookRoutine(List<Sprite> images)
    {
        int index = 0;
        while (true)
        {
            if (images == null || images.Count == 0)
            {
                characterImage.sprite = null;
                yield break;
            }

                characterImage.sprite = images[index];
            index = (index + 1) % images.Count;

            yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator ShowCombatLog(string message)
    {
        //combatLogPanel.SetActive(true);
        combatLogText.text = message;

        while (Input.GetMouseButton(0))
            yield return null; // wait for release

        // Wait until the player clicks
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        //combatLogPanel.SetActive(false);
        combatLogText.text = "";

        while (Input.GetMouseButton(0))
            yield return null;

        yield return null;
    }

    private void ClearAllPedestals()
    {
        foreach (var kv in pedestalMap)
            if (kv.Value) Destroy(kv.Value.gameObject);
        pedestalMap.Clear();
    }

    private Vector3[] ComputeRowPositionsLocal(Transform anchor, int count, float spacing, Vector3 dirLocal)
    {
        var result = new Vector3[count];
        if (count <= 0) return result;

        // Normalize the direction so "spacing" is in world units
        Vector3 dir = dirLocal.normalized;

        // symmetric spread around the anchor origin
        float start = -(count - 1) * 0.5f * spacing;
        for (int i = 0; i < count; i++)
            result[i] = dir * (start + i * spacing);   // LOCAL positions

        return result;
    }

    private void SpawnAllPedestals()
    {
        ClearAllPedestals();

        // --- Players: forward + right ---
        var players = turnOrder.Where(t => t.isPlayerControlled).ToList();
        var pLocal = ComputeRowPositionsLocal(playerRowAnchor, players.Count, rowSpacing, new Vector3(1, 0, 1));

        for (int i = 0; i < players.Count; i++)
        {
            var go = Instantiate(pedestalPrefab, playerRowAnchor);   // parent first
            go.transform.localPosition = pLocal[i];
            go.transform.localRotation = Quaternion.identity;        // or face anchor.forward if you prefer
            go.transform.localScale = new Vector3(2, 1, 2);

            var pc = go.GetComponent<PedestalController>();
            pc.Bind(players[i]);
            pedestalMap[players[i]] = pc;
        }

        // --- Enemies: forward + left ---
        var enemies = turnOrder.Where(t => !t.isPlayerControlled).ToList();
        var eLocal = ComputeRowPositionsLocal(enemyRowAnchor, enemies.Count, rowSpacing, new Vector3(-1, 0, 1));

        for (int i = 0; i < enemies.Count; i++)
        {
            var go = Instantiate(pedestalPrefab, enemyRowAnchor);    // parent first
            go.transform.localPosition = eLocal[i];
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = new Vector3(2, 1, 2);

            var pc = go.GetComponent<PedestalController>();
            pc.Bind(enemies[i]);
            pedestalMap[enemies[i]] = pc;
        }
    }


    public void RefreshUIFor(params Combatant[] who)
    {
        print("I was called");
        foreach (var c in who)
            if (c != null && pedestalMap.TryGetValue(c, out var ped) && ped) ped.Refresh();
    }

    public void AddCharacter(string name)
    {
        foreach(var combatant in allCharacters)
        {
            if (combatant.name == name)
            {
                playerCharacters.Add(combatant);
            }
        }
    }

    public void RemoveCharacter(string name)
    {
        foreach(var combatant in playerCharacters)
        {
            if (combatant.name == name)
            {
                playerCharacters.Remove(combatant);
            }
        }
    }

    public void PlaySkillSfx(SkillData skill)
    {
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(skill.castSFX, skill.sfxVolume);
        } 
    }

    private List<LevelUpSummary> BuildLevelUpSummaries(List<Combatant> survivors,
                                                   Dictionary<CharacterData, CharacterStatsSnapshot> beforeMap,
                                                   int xpPerMember)
    {
        var list = new List<LevelUpSummary>();
        foreach (var c in survivors)
        {
            var before = beforeMap[c.data];
            var after = CharacterStatsSnapshot.Capture(c.data);
            list.Add(new LevelUpSummary
            {
                character = c.data,
                xpGained = xpPerMember,
                before = before,
                after = after,
                xpNeededToNext = c.data.XPNeededToNextLevel()
            });
        }
        // Only show those who actually changed XP (everyone) — you can also filter to LevelsGained > 0 if you prefer
        return list;
    }

}
