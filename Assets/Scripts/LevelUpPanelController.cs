using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanelController : MonoBehaviour
{
    [Header("Wiring")]
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI xpToNextText;


    [Header("Stat Rows (optional hide if no change)")]
    [SerializeField] private GameObject hpRow; [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private GameObject mpRow; [SerializeField] private TextMeshProUGUI mpText;
    [SerializeField] private GameObject strRow; [SerializeField] private TextMeshProUGUI strText;
    [SerializeField] private GameObject perRow; [SerializeField] private TextMeshProUGUI perText;
    [SerializeField] private GameObject evaRow; [SerializeField] private TextMeshProUGUI evaText;
    [SerializeField] private GameObject spiRow; [SerializeField] private TextMeshProUGUI spiText;
    [SerializeField] private GameObject spdRow; [SerializeField] private TextMeshProUGUI spdText;

    [SerializeField] private Button nextButton;

    private bool _clicked;

    private void Awake()
    {
        if (root != null) root.SetActive(false);
        if (nextButton != null) nextButton.onClick.AddListener(() => _clicked = true);
    }

    public IEnumerator ShowSequence(List<LevelUpSummary> summaries)
    {
        if (summaries == null || summaries.Count == 0) yield break;

        root.SetActive(true);

        foreach (var s in summaries)
        {
            // Fill header
            nameText.text = s.character.characterName;
            levelText.text = s.LevelsGained > 0
                ? $"Level: {s.before.level} → {s.after.level}  (+{s.LevelsGained})"
                : $"Level: {s.after.level}";
            xpText.text = $"+{s.xpGained} XP";
            xpToNextText.text = $"XP to next level: {s.xpNeededToNext}";


            // Stat row helper
            void SetRow(GameObject row, TextMeshProUGUI txt, int before, int after, string label)
            {
                int delta = after - before;
                bool changed = delta != 0;
                if (row) row.SetActive(changed);
                if (changed && txt) txt.text = $"{label}: {before} → {after}  ({(delta >= 0 ? "+" : "")}{delta})";
            }

            SetRow(hpRow, hpText, s.before.maxHP, s.after.maxHP, "HP");
            SetRow(mpRow, mpText, s.before.maxMP, s.after.maxMP, "MP");
            SetRow(strRow, strText, s.before.strength, s.after.strength, "STR");
            SetRow(perRow, perText, s.before.perception, s.after.perception, "PER");
            SetRow(evaRow, evaText, s.before.evasiveness, s.after.evasiveness, "EVA");
            SetRow(spiRow, spiText, s.before.spirit, s.after.spirit, "SPI");
            SetRow(spdRow, spdText, s.before.speed, s.after.speed, "SPD");

            // Wait for click
            _clicked = false;
            while (!_clicked) yield return null;
        }

        root.SetActive(false);
    }
}
