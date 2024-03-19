using UnityEngine;

//In Player's SkillSlot SkillStateManager
//Each PlayerSkill has a PlayerSkillStateManager
public class PlayerSkillStateManager : SkillStateManager
{
    [Header("States")]

    //All the possible states of a skill
    public PlayerUnusuable UnusuableState = new PlayerUnusuable();
    public PlayerUsable UsableState = new PlayerUsable();
    public  PlayerInUse InUseState = new PlayerInUse();
}
