using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillManager))]
public class SkillManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SkillManager skillManager = (SkillManager)target;

        GUILayout.Space(10); // Add some space

        GUILayout.Label("Skill Actions", EditorStyles.boldLabel); // Header label

        if (GUILayout.Button("Search Skill"))
        {
            skillManager.StartSkillManager();
        }
    }
}
