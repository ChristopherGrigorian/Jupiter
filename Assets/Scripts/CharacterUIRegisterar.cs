using UnityEngine;

public class CharacterUIRegistrar : MonoBehaviour
{
    [SerializeField] private CombatHudManager hud;   // assign in prefab (or auto-find)

    private Combatant owner; // who this HUD belongs to

    public void RegisterFor(Combatant c)
    {
        owner = c;

        // lazy find if not assigned
        if (!hud) hud = GetComponentInChildren<CombatHudManager>(true);

        // only register HUDs for player-controlled units
        if (hud && c != null && c.isPlayerControlled && GameController.Instance != null)
            GameController.Instance.RegisterHud(c, hud);
    }

    private void OnDestroy()
    {
        if (owner != null && GameController.Instance != null)
            GameController.Instance.UnregisterHud(owner);
    }
}
