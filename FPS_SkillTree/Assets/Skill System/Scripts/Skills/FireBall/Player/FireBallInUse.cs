using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallInUse : PlayerInUse
{
    public override void EnterState(PlayerFireballStateManager skill)
    {
        base.EnterState(skill);
        UseSKill();

        ExitState(skill);
    }

    public override void ExitState(PlayerFireballStateManager skill)
    {
        skill.SwitchState(skill.SwitchState(skill.FireballUnusuableState);

    }

    public override void UseSKill()
    {
        base.UseSKill();
        Debug.Log("Fireball");
    }
}
