using UnityEngine;

public class SkillTreeBuilder : MonoBehaviour
{
    public SkillManager skillManager;

    public void PrintSkillTree()
    {
        // Get all skills from the SkillManager
        var allSkills = skillManager.GetAllSkills();

        Debug.Log("===== Skill Tree =====");
        foreach (var skill in allSkills)
        {
            PrintSkillNode(skill, 0);
        }
        Debug.Log("===== End of Skill Tree =====");
    }

    void PrintSkillNode(SkillData skill, int indentLevel)
    {
        string indent = new string(' ', indentLevel * 4);
        Debug.Log($"{indent}Skill Name: {skill.Name}");
        Debug.Log($"{indent} - Order: {skill.Order}");
        Debug.Log($"{indent} - Cost: {skill.Cost}");
        Debug.Log($"{indent} - Type: {skill.Type}");
        Debug.Log($"{indent} - Description: {skill.Description}");
        Debug.Log("----------------------");

        foreach (var childSkill in skill.Children)
        {
            PrintSkillNode(childSkill, indentLevel + 1);
        }
    }
}
