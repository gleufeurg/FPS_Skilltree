using Unity.VisualScripting;
using UnityEngine;

public class GameTreeUnlocked : GameTreeSkillState
{
    public override void EnterState(SkillStateManager skill)
    {
        Debug.Log("I grant you my power !");
    }

    public override void UpdateState(SkillStateManager skill) { }
    public override void ExitState(SkillStateManager skill) { }
}
