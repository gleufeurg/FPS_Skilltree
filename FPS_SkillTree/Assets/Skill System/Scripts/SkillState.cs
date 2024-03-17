using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generic Class
//Ultra Generic SkillState Class, used by : StartSkillState, GameTreeSkillState, PlayerSkillState
public abstract class SkillState
{
    public abstract void EnterState(SkillStateManager skill);
    public abstract void UpdateState(SkillStateManager skill);
    public abstract void ExitState(SkillStateManager skill);
}
