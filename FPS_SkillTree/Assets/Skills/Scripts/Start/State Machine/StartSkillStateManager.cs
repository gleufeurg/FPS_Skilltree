using UnityEngine;

//Before start SkillStateManager
//Each StartSkill has a StartSkillStateManager
public class StartSkillStateManager : SkillStateManager
{
    [Header ("States")]
    StartSkillState currentState;

    //All the possible states of a skill
    public StartFullyLocked FullyLockedState = new StartFullyLocked();
    public StartUnlockable UnlockableState = new StartUnlockable();
    public StartUnlocked UnlockedState = new StartUnlocked();

    void Start()
    {
        //Starting state
        currentState = FullyLockedState;
        //Reference this skill state manager
        currentState.EnterState(this);
    }

    //Current state logic is played here
    void Update()
    {
        currentState.UpdateState(this);
    }

    //Switch between different states
    public void SwitchState(StartSkillState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void OnButtonClick()
    {
        currentState.OnClick(this);
    }
}
