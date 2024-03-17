using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class StartUnlocked : StartSkillState
{

    public override void EnterState(SkillStateManager skill)
    {
        Debug.Log("I am Unlocked !");
    }

    public override void UpdateState(SkillStateManager skill) { }
    public override void ExitState(SkillStateManager skill) { }

    public override void OnClick(StartSkillStateManager skill)
    {
        Debug.Log("Ouch ! Hey ! I am already Unlocked !");
    }
}
