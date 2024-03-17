using UnityEngine;

//In game SkillStateManager
//Each GameTreeSkill has a GameTreeSkillStateManager
public class GameTreeSkillStateManager : SkillStateManager
{
    [Header("States")]
    GameTreeSkillState currentState;

    //All the possible states of a skill
    public GameTreeUnlockable UnlockableState = new GameTreeUnlockable();
    public GameTreeUnlocked UnlockedState = new GameTreeUnlocked();

    void Start()
    {
        //Starting state
        currentState = UnlockableState;
        //Reference this skill state manager
        currentState.EnterState(this);
    }

    //Current state logic is played here
    void Update()
    {
        currentState.UpdateState(this);
    }

    //Switch between different states
    public void SwitchState(GameTreeSkillState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
