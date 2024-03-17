using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillManager))]
public class SkillManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SkillManager skillManager = (SkillManager)target;

        GUILayout.Space(10);

        GUILayout.Label("Search Skill by Name", EditorStyles.boldLabel);

        skillManager.skillNameToSearch = EditorGUILayout.TextField("Skill Name to Search:", skillManager.skillNameToSearch);

        GUILayout.Space(5);

        if (GUILayout.Button("Search Skill by Name"))
        {
            SkillData foundSkill = skillManager.FindSkillByName(skillManager.skillNameToSearch);

            if (foundSkill != null)
            {
                Debug.Log($"Skill found - Name: {foundSkill.Name}, Order: {foundSkill.Order}, Cost: {foundSkill.Cost}, Type: {foundSkill.Type}, Description: {foundSkill.Description}");
            }
            else
            {
                Debug.Log($"Skill with name '{skillManager.skillNameToSearch}' not found.");
            }
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Print Entire Skill Tree"))
        {
            foreach (SkillData skill in skillManager.GetAllSkills())
            {
                Debug.Log($"Skill - Name: {skill.Name}, Order: {skill.Order}, Cost: {skill.Cost}, Type: {skill.Type}, Description: {skill.Description}");
            }
        }
    }
}
