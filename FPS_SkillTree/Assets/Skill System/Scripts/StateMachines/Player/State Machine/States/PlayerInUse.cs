using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skill has been activated and is currently used by the player
public class PlayerInUse : PlayerSkillState
{
    public override void EnterState(SkillStateManager skill)
    {
        Debug.Log("YEAAAAH ! Kill'em all mwahahaaaah !");
    }

    public override void UpdateState(SkillStateManager skill) { }
    public override void ExitState(SkillStateManager skill) { }
    public override void UseSKill() { }
}
