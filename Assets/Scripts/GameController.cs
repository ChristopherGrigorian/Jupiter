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

    [SerializeField] private GameObject combatHUD;
    [SerializeField] private GameObject dialogueHUD;
    [SerializeField] private Transform targetContainer;

    [SerializeField] private GameObject combatantButtonPrefab;

    [SerializeField] private GameObject combatLogPanel;
    [SerializeField] private TextMeshProUGUI combatLogText;

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
    [SerializeField] private float rowSpacing = 100.0f; // distance between pedestals

    private readonly Dictionary<Combatant, PedestalController> pedestalMap = new();

    private readonly Dictionary<Combatant, CombatHudManager> hudByCombatant
     = new Dictionary<Combatant, CombatHudManager>();


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

    private Transform lastActorViewPoint;


    [Header("Environment")]
    [SerializeField] private Material combatSkybox;   // assign your combat skybox

    // saved originals
    private Material _origSkybox;
    private bool _origFogEnabled;
    private Color _origFogColor;
    private float _origFogDensity;

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

        _origSkybox = RenderSettings.skybox;
        _origFogEnabled = RenderSettings.fog;
        _origFogColor = RenderSettings.fogColor;
        _origFogDensity = RenderSettings.fogDensity;
    }


    public void StartCombat(string encounterID, string continueKnot)
    {
        ApplyCombatEnvironment();
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
        HideAllPlayersUI(instant: true);

        currentTurnIndex = 0;

        StartCoroutine(CombatLoop());
    }

    private void PrepareCombatants()
    {
        turnOrder.Clear();

        var equippedPlayers = playerCharacters
           .Where(p => p.currentlyEquipped)
           .Take(3)
           .ToList();

        if (equippedPlayers.Count == 0)
        {
            Debug.LogWarning("No equipped player characters selected. Aborting combat start.");
            combatActive = false;
            return;
        }

        foreach (var p in equippedPlayers)
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
        yield return StartCoroutine(ShowCombatLog($"Ready for combat? Click to continue!"));
        Debug.Log("EnteredCombatLoop");
        while (combatActive) {
            Combatant current = turnOrder[currentTurnIndex];
            FocusCameraOn(current);
            if (current.isPlayerControlled)
                ShowOnlyCurrentPlayerUI(current, instant: false);
            else
                HideAllPlayersUI(instant: false);

            if (current.currentHP > 0)
            {
                Debug.Log($"It is now {current.Name}'s turn.");

                CombatantName.text = "Current Combatant: " + current.Name;
                CombatantHealth.text = "Combatant HP: " + current.currentHP.ToString();
                CombatantMP.text = "Combatant MP: " + current.currentMP.ToString();

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

                RestoreEnvironment();

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
            Debug.Log($"Player {player.Name}'s turn. Choose a skill.");

            var hud = GetHudFor(player);
            if (hud == null)
            {
                Debug.LogError($"No HUD registered for {player.Name}. Did you add CharacterUIRegistrar?");
                yield break;
            }

            ShowOnlyHudFor(player);

            SkillData chosenSkill = null;

            // Populate the action UI and define what happens on click
            hud.PopulateActions(player.data, (SkillData skill) =>
            {

                if (!CheckMPReqMet(player, skill))
                {
                    Debug.LogWarning("Not enough MP! Choose a different skill.");
                    return; // Don't end the turn, just ignore this selection
                }

                Debug.Log($"{player.Name} uses {skill.skillName}!");
                chosenSkill = skill;
                hud.SetAllActionButtonsInteractable(false);
            });

            yield return new WaitUntil(() => chosenSkill != null);
            //combatHUD.SetActive(false);
            yield return StartCoroutine(TargetSelection(player, chosenSkill));
            //combatHUD.SetActive(true);

            if (selectionCanceled || (!chosenSkill.isAOE && target == null))
            {
                hud.PendingConsumeAction = null;
                hud.SetAllActionButtonsInteractable(true);
                chosenSkill = null;
                continue;
            }

            hud.PendingConsumeAction?.Invoke();
            hud.PendingConsumeAction = null;

            SpendMP(player, chosenSkill);
            yield return StartCoroutine(PlayerAction(player, chosenSkill));
            player.currentMP += 1;

            yield return StartCoroutine(ResolveEndOfTurnStatuses(player));

            if (hud) hud.ClearAll();
            yield break;
        }
    }

    private IEnumerator PlayerAction(Combatant player, SkillData chosenSkill)
    {
        List<Combatant> targets;
        if (chosenSkill.isAOE)
            targets = EnumerateValidTargets(chosenSkill.targetsEnemies).ToList();
        else
            targets = target != null ? new List<Combatant> { target } : new List<Combatant>();

        switch (chosenSkill.type)
        {
            case SkillType.Attack:
                PlaySkillSfx(chosenSkill);

                foreach (var t in targets)
                {
                    if (!t.IsAlive) continue;
                    int damage = chosenSkill.power;
                    string critMessage = "";
                    if (RollToHit(player.EffectivePerception, t.EffectiveEvasiveness))
                    {
                        PlaySkillSfx(chosenSkill);
                        if (RollToCrit(player.EffectiveSpirit))
                        {
                            critMessage = $"{player.data.characterName}'s attack was a critical hit! ";
                            damage *= 2;
                        }
                        int totalDamage = damage + (player.EffectiveStrength / 2) + player.equippedWeapon.GrabWeaponPower();
                        t.currentHP = Mathf.Max(0, t.currentHP - totalDamage);
                        RefreshUIFor(t);
                        yield return StartCoroutine(ShowCombatLog(critMessage + $"{t.Name} takes {totalDamage} damage!"));
                    }
                    else
                    {
                        sfxSource.PlayOneShot(t.data.dodgeSound);
                        yield return StartCoroutine(ShowCombatLog($"{t.Name} dodges the attack!"));
                    }

                    yield return StartCoroutine(TryApplyStatuses(player, t, chosenSkill));
                }
                break;
            case SkillType.Heal:
                PlaySkillSfx(chosenSkill);
                foreach (var t in targets)
                {
                    if (!t.IsAlive) continue;
                    int heal = chosenSkill.potency;
                    t.currentHP = Mathf.Min(t.EffectiveMaxHP, t.currentHP + heal);
                    RefreshUIFor(t);
                    yield return StartCoroutine(ShowCombatLog($"{player.Name} healed {t.Name} for {heal}."));
                    yield return StartCoroutine(TryApplyStatuses(player, t, chosenSkill));
                }
                break;
            case SkillType.Buff:
                PlaySkillSfx(chosenSkill);
                foreach (var t in targets)
                {
                    if (!t.IsAlive) continue;
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
                    RefreshUIFor(t);
                    yield return StartCoroutine(ShowCombatLog($"{player.Name} utilized {chosenSkill.name}"));
                    yield return StartCoroutine(TryApplyStatuses(player, t, chosenSkill));
                }
                break;
            case SkillType.Debuff:
                PlaySkillSfx(chosenSkill);
                foreach (var t in targets)
                {
                    if (!t.IsAlive) continue;
                    List<StatusToApply> statusEffect = chosenSkill.statusesToApply;
                    foreach (var s in statusEffect)
                    {
                        for (int i = 0; i < s.status.statModifiers.Length; i++)
                        {
                            var mod = s.status.statModifiers[i];
                            mod.flatDelta = chosenSkill.potency;
                            mod.effectiveFlatDelta = mod.flatDelta;
                            s.status.statModifiers[i] = mod;
                        }
                    }
                    RefreshUIFor(t);
                    yield return StartCoroutine(ShowCombatLog($"{player.Name} utilized {chosenSkill.name}"));
                    yield return StartCoroutine(TryApplyStatuses(player, t, chosenSkill));
                }
                break;
            default:
                Debug.Log("That wasn't an attack ability.");
                break;
        }
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

                var inst = target.statuses.LastOrDefault(st => st.data == s.status);
                if (inst != null)
                {
                    inst.skipFirstTick = ReferenceEquals(source, target);
                }
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

            if (s.skipFirstTick)
            {
                s.skipFirstTick = false;
            }
            else
            {
                s.remainingTurns--;
            }
            
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

    private IEnumerable<Combatant> EnumerateValidTargets(bool targetsEnemies)
    {
        return turnOrder.Where(t => t.IsAlive && (targetsEnemies ? !t.isPlayerControlled : t.isPlayerControlled));
    }

    private IEnumerator EnemyTurn(Combatant enemy)
    {

        var hud = GetHudFor(enemy);
        if (hud) hud.SetAllActionButtonsInteractable(false);

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
        if (!skill.targetsEnemies)
        {
            // Ally targeting mode: show all player UIs
            ShowAllPlayersUI(true, instant: false);
        }

        target = null;

        var playerHud = GetHudFor(player);
        var playerFader = GetFaderFor(player);

        // snapshot if it was visible
        bool hudWasVisible =
            (playerFader && playerFader.gameObject.activeSelf && playerFader.GetComponent<CanvasGroup>().alpha > 0.01f)
            || (playerHud && playerHud.gameObject.activeSelf);

        if (hudWasVisible)
        {
            if (playerFader) playerFader.Hide();
            else playerHud.gameObject.SetActive(false);
        }


        if (cameraPan != null)
        {
            if (skill.targetsEnemies && enemyTargetCamAnchor)
                cameraPan.PanTo(enemyTargetCamAnchor);
            else if (!skill.targetsEnemies && allyTargetCamAnchor)
                cameraPan.PanTo(allyTargetCamAnchor);
        }

        PedestalController lastHover = null;

        if (skill.isAOE)
        {
            HighlightGroup(skill.targetsEnemies, true);

            // confirm with LMB, cancel with RMB/Esc
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    selectionCanceled = false;
                    break;
                }
                if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
                {
                    selectionCanceled = true;
                    break;
                }
                yield return null;
            }

            // cleanup
            HighlightGroup(skill.targetsEnemies, false);
            if (selectionCanceled)
            {
                if (cameraPan != null && lastActorViewPoint != null)
                    cameraPan.PanTo(lastActorViewPoint);     // back to actor’s pedestal
                ShowOnlyCurrentPlayerUI(player, instant: false);
                if (hudWasVisible)
                {
                    if (playerFader) playerFader.Show();
                    else playerHud.gameObject.SetActive(true);
                }
            }
            else
            {
                if (cameraPan != null && combatCamAnchor != null)
                    cameraPan.PanTo(combatCamAnchor);         // logs view
            }
            yield break; // target remains null on purpose for AOE
        }

        while (target == null)
        {
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
        SetHover(lastHover, false);
        lastHover = null;

        if (selectionCanceled)
        {
            
            if (cameraPan != null && lastActorViewPoint != null) cameraPan.PanTo(lastActorViewPoint);
            ShowOnlyCurrentPlayerUI(player, instant: false);
            if (hudWasVisible)
            {
                if (playerFader) playerFader.Show();
                else if (playerHud) playerHud.gameObject.SetActive(true);
            }
        }
        else
        {
            if (cameraPan != null && combatCamAnchor != null) cameraPan.PanTo(combatCamAnchor);
            // keep HUD hidden during combat log/resolve; next actor’s ShowOnlyHudFor() will show theirs
        }
    }

    private void SetHover(PedestalController ped, bool on)
    {
        if (ped) ped.SetHover(on);
    }

    private void HighlightGroup(bool targetsEnemies, bool on)
    {
        foreach (var kv in pedestalMap)
        {
            var c = kv.Key;
            var ped = kv.Value;
            if (!c.IsAlive) continue;

            bool isValid = targetsEnemies ? !c.isPlayerControlled : c.isPlayerControlled;
            if (isValid) ped.SetHover(on);
        }
    }


    public IEnumerator ShowCombatLog(string message)
    {
        ShowAllPlayersUI(true, instant: false);

        if (cameraPan != null && combatCamAnchor != null)
            cameraPan.PanTo(combatCamAnchor);
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
            go.transform.localRotation = Quaternion.Euler(0, -45, 0);       // or face anchor.forward if you prefer
            go.transform.localScale = new Vector3(1, 0.5f, 1);

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
            go.transform.localRotation = Quaternion.Euler(0, 45, 0);
            go.transform.localScale = new Vector3(1, 0.5f, 1);

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
        foreach (var combatant in allCharacters)
        {
            if (combatant.characterName == name) // prefer a dedicated field over Unity's .name
            {
                if (playerCharacters.Contains(combatant)) return; // avoid dupes

                bool canEquip = playerCharacters.Count(c => c.currentlyEquipped) < 3;

                combatant.currentlyEquipped = canEquip; // only equip if room
                playerCharacters.Add(combatant);
                break;
            }
        }
    }


    public void RemoveCharacter(string name)
    {
        int i = playerCharacters.FindIndex(c => c.characterName == name);
        if (i >= 0) playerCharacters.RemoveAt(i);
    }


    public void PlaySkillSfx(SkillData skill)
    {
        if (sfxSource != null && skill.castSFX != null)
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
        // Only show those who actually changed XP (everyone) � you can also filter to LevelsGained > 0 if you prefer
        return list;
    }


    public void RegisterHud(Combatant c, CombatHudManager hud)
    {
        hudByCombatant[c] = hud;
    }

    public void UnregisterHud(Combatant c)
    {
        hudByCombatant.Remove(c);
    }

    private CombatHudManager GetHudFor(Combatant c)
    {
        hudByCombatant.TryGetValue(c, out var hud);
        return hud;
    }

    private void ShowOnlyHudFor(Combatant c)
    {
        // fade-hide everyone else
        foreach (var kv in hudByCombatant.ToList())
        {
            var other = kv.Key;
            var hud = kv.Value;
            if (!hud) continue;

            var fader = hud.GetComponent<UIFader>();
            bool isActive = hud.gameObject.activeInHierarchy;

            if (other == c)
            {
                if (fader)
                {
                    if (isActive) fader.Show();
                    else { hud.gameObject.SetActive(true); fader.SetVisibleImmediate(true); }
                }
                else hud.gameObject.SetActive(true);
            }
            else
            {
                if (fader)
                {
                    if (isActive) fader.Hide();
                    else fader.SetVisibleImmediate(false); // don’t start coroutine on inactive
                }
                else hud.gameObject.SetActive(false);
            }
        }
    }

    private PedestalController GetPedestalFor(Combatant c)
    {
        if (pedestalMap.TryGetValue(c, out var ped))
            return ped;
        return null;
    }

    private void FocusCameraOn(Combatant c)
    {
        var ped = GetPedestalFor(c);
        if (ped != null && ped.viewingPoint != null && cameraPan != null)
        {
            lastActorViewPoint = ped.viewingPoint;
            cameraPan.PanTo(ped.viewingPoint);
        }
    }

    private UIFader GetFaderFor(Combatant c)
    {
        var hud = GetHudFor(c);
        return hud ? hud.GetComponent<UIFader>() : null;
    }

    private void ShowPlayerPedestalsWhere(System.Func<Combatant, bool> predicate, bool instant = false)
    {
        foreach (var kv in pedestalMap)
        {
            var c = kv.Key;
            var ped = kv.Value;
            if (!c.isPlayerControlled || ped == null) continue;
            ped.SetUIVisible(predicate(c), instant);
        }
    }

    // Convenience wrappers
    private void ShowOnlyCurrentPlayerUI(Combatant current, bool instant = false)
    {
        ShowPlayerPedestalsWhere(c => c == current && c.IsAlive, instant);
    }

    private void ShowAllPlayersUI(bool on, bool instant = false)
    {
        ShowPlayerPedestalsWhere(c => on && c.IsAlive, instant);
    }

    private void HideAllPlayersUI(bool instant = false)
    {
        ShowPlayerPedestalsWhere(c => false, instant);
    }

    private void ApplyCombatEnvironment()
    {
        if (combatSkybox) RenderSettings.skybox = combatSkybox;

        // optional: punch up atmosphere during combat
        // RenderSettings.ambientMode = AmbientMode.Skybox;
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.05f, 0.08f, 0.12f);
        RenderSettings.fogDensity = 0.02f;

        DynamicGI.UpdateEnvironment(); // refresh ambient/probe lighting from new skybox
    }

    private void RestoreEnvironment()
    {
        RenderSettings.skybox = _origSkybox;
        RenderSettings.fog = _origFogEnabled;
        RenderSettings.fogColor = _origFogColor;
        RenderSettings.fogDensity = _origFogDensity;

        DynamicGI.UpdateEnvironment();
    }
}
