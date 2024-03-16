using Unity.VisualScripting;
using UnityEngine;

//Generic class
//Each state logic will be played by a child of this class, protecting others state from unwanted complexity, changes and dependancies
public abstract class StartSkillState : SkillState
{
    public abstract void OnClick(StartSkillStateManager skill);
}