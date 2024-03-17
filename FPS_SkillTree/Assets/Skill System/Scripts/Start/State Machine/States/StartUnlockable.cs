using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUnlockable : StartSkillState
{
    public override void EnterState(SkillStateManager skill)
    {
        Debug.Log("I am now unlockable");
    }

    public override void UpdateState(SkillStateManager skill) { }
    public override void ExitState(SkillStateManager skill) { }

    public override void OnClick(StartSkillStateManager skill)
    {
        skill.SwitchState(skill.UnlockedState);
    }

}
