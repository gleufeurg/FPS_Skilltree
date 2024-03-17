using UnityEngine;
using UnityEngine.UI;

//Generic Class
//This is the real deal, this is a skill unlocked from the GameTreeSkillTree
//This is placed inside a SkillSlot
public abstract class PlayerSkill : Skill
{
    public abstract void UseSkill();
}

//Just an idea of what it might be, not used for now so feel free to delete this
[System.Serializable]
public struct PlayerSkillstr
{
    public Image icone;

    public float cooldown;
    public float startDuration;
    public float duration;
    public float endDuration;
}
