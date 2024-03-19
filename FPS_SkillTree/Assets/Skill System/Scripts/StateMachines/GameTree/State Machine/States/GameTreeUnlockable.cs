using UnityEngine;

public class GameTreeUnlockable : GameTreeSkillState
{
    public override void EnterState(SkillStateManager skill)
    {
        Debug.Log("Try to unlock me");
    }

    public override void UpdateState(SkillStateManager skill)
    {

    }
    public override void ExitState(SkillStateManager skill) { }
}
