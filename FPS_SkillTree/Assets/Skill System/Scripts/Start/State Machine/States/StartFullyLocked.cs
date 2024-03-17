using UnityEngine;

public class StartFullyLocked : StartSkillState
{
    public override void EnterState(SkillStateManager skill)
    {
        Debug.Log("I am locked...");
    }

    public override void UpdateState(SkillStateManager skill) { }
    public override void ExitState(SkillStateManager skill) { }

    public override void OnClick(StartSkillStateManager skill)
    {
        skill.SwitchState(skill.UnlockableState);
    }
}
