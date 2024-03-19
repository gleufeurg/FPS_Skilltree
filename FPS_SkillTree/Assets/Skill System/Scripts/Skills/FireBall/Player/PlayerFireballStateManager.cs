using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireballStateManager : PlayerSkillStateManager
{
    [Header("States")]
    PlayerSkillState currentState;

    public FireballUnusuable FireballUnusuableState;
    public FireballUsable FireballUsableState;
    public FireBallInUse FireballInUseState;

    void Start()
    {
        //Beware to set currentState
        currentState = FireballUnusuableState;
        Debug.Log("SkillStateManager");

        //Reference this skill state manager
        currentState.EnterState(this);
    }

    //Current state logic is played here
    void Update()
    {
        currentState.UpdateState(this);
    }

    //Switch between different states
    public void SwitchState(PlayerSkillState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
