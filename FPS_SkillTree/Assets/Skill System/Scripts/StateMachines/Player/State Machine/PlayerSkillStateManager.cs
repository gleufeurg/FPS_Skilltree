using UnityEngine;


//In Player's SkillSlot SkillStateManager
//Each PlayerSkill has a PlayerSkillStateManager
public class PlayerSkillStateManager : SkillStateManager
{
    [Header("States")]
    PlayerSkillState currentState;

    //All the possible states of a skill
    public PlayerUnusuable UnusuableState = new PlayerUnusuable();
    public PlayerUsable UsableState = new PlayerUsable();
    public  PlayerInUse InUseState = new PlayerInUse();

    void Start()
    {
        //Starting state
        currentState = UnusuableState;
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
