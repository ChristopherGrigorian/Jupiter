using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI 
{
    private Combatant self;
    private List<Combatant> turnOrder;
    private EnemyAIState state = EnemyAIState.Decide;

    private SkillData chosenSkill;
    private Combatant chosenTarget;

    public EnemyAI(Combatant self, List<Combatant> turnOrder)
    {
        this.self = self;
        this.turnOrder = turnOrder;
    }

    public IEnumerator Run()
    {
        while (state != EnemyAIState.Done)
        {
            switch(state)
            {
                case EnemyAIState.Decide:
                    yield return DecideAction();
                    break;
                case EnemyAIState.ChooseSkill:
                    yield return ChooseSkill();
                    break;
                case EnemyAIState.ChooseTarget:
                    yield return ChooseTarget();
                    break;
                case EnemyAIState.Execute:
                    yield return Execute();
                    break;

            }
            yield return null;
        }
    }

    private IEnumerator DecideAction()
    {
        state = EnemyAIState.ChooseSkill;
        yield return null;
    }

    private IEnumerator ChooseSkill()
    {
        foreach (var skill in self.GetAvailableSkills())
        {
            if (self.currentMP >= skill.MPCost)
            {
                chosenSkill = skill;
                self.currentMP -= skill.MPCost;
                break;
            }
        }

        if (chosenSkill == null)
        {
            Debug.Log($"{self.Name} has no usuable skills!");
            state = EnemyAIState.Done;
            yield break;
        }

        state = EnemyAIState.ChooseTarget;
        yield return null;
    }

    private IEnumerator ChooseTarget()
    {
        if (chosenSkill.targetsEnemies)
        {
            chosenTarget =  turnOrder.Find(t => t.isPlayerControlled && t.IsAlive);
        } else
        {
            chosenTarget = self;
        }
        
        if (chosenTarget == null)
        {
            Debug.Log($"{self.Name} has no valid targets.");
            state = EnemyAIState.Done;
            yield break;
        }

        yield return GameController.Instance.StartCoroutine(GameController.Instance.ShowCombatLog($"{self.Name} targets {chosenTarget.Name} with {chosenSkill.name}."));
        state = EnemyAIState.Execute;
        yield return null;
    }

    private IEnumerator Execute()
    {
        string message = "";
        //message = $"{self.Name} uses {chosenSkill.skillName} on {chosenTarget.Name}";
        //yield return GameController.Instance.StartCoroutine(GameController.Instance.ShowCombatLog(message));

        

        switch (chosenSkill.type)
        {
            case SkillType.Attack:
                if (GameController.Instance.RollToHit(self.EffectivePerception, chosenTarget.EffectiveEvasiveness))
                {
                    GameController.Instance.PlaySkillSfx(chosenSkill);
                    int totalDamage = chosenSkill.power + (self.EffectiveStrength / 2);
                    chosenTarget.currentHP -= totalDamage;
                    GameController.Instance.RefreshUIFor(chosenTarget);
                    message = $"{chosenTarget.Name} takes {totalDamage} damage.";
                    yield return GameController.Instance.StartCoroutine(GameController.Instance.TryApplyStatuses(self, chosenTarget, chosenSkill));
                } else
                {
                    GameController.Instance.sfxSource.PlayOneShot(chosenTarget.data.dodgeSound);
                    message = $"{chosenTarget.Name} dodges the attack!";
                }
                break;
            case SkillType.Heal:
                GameController.Instance.PlaySkillSfx(chosenSkill);
                chosenTarget.currentHP += chosenSkill.potency;
                message = $"{chosenTarget.Name} heals for {chosenSkill.potency}.";
                yield return GameController.Instance.StartCoroutine(GameController.Instance.TryApplyStatuses(self, chosenTarget, chosenSkill));
                break;
            default:
                message = "Unknown skill type.";
                break;
        }

        yield return GameController.Instance.StartCoroutine(GameController.Instance.ShowCombatLog(message));
        yield return new WaitForSeconds(1f);
        self.currentMP += 1;
        state = EnemyAIState.Done;
    }
}
