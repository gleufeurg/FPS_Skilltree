using UnityEngine;

//Generic Class
//This is a Skill in the StartSkillTree
//It DOES NOT serve in game, only before, when the player make his SkillTree
public class StartSkill : Skill
{

}

//Just an idea of what it might be, not used for now so feel free to delete this
[System.Serializable]
public struct StartSkillstr
{
    public string skillName;
    public string description;

    public int cost;
    public int order;

    public StartSkill[] connections;
}
