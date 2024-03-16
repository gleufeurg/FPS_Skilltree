using UnityEngine;

//Generic Class
//This is a skill in the In game SkillTree
//It is NOT a SkillTree and IS NOT usable by the Player, this the rôle of PlayerSkill
public class GameTreeSkill : Skill
{

}

//Just an idea of what it might be, not used for now so feel free to delete this
[System.Serializable]
public struct GameTreeSkillstr
{
    public string skillName;
    public int order;

    //public Challenge challenge; the actual challenge of this skill (if there is no challenge then trhe challenge = fakeChallenge)

    public StartSkill[] connections;
}
