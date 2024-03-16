using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The skill can't be used for the moment
//Because it is indexer cooldown or the player is a certain status making him unable to use it
public class PlayerUnusuable : PlayerSkillState
{
    public override void EnterState(SkillStateManager skill)
    {
        Debug.Log("I can't be used for the moment");
    }

    public override void UpdateState(SkillStateManager skill) { }
    public override void ExitState(SkillStateManager skill) { }
}
