using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUsable : PlayerSkillState
{
    public override void EnterState(SkillStateManager skill)
    {
        Debug.Log("Use my full power to destroy your ennemies !");
    }

    public override void UpdateState(SkillStateManager skill) { }
    public override void ExitState(SkillStateManager skill) { }
    public override void UseSKill() { }
}
