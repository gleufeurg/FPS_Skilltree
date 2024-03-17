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

        if (GUILayout.Button("Load Skills from CSV"))
        {
            skillManager.LoadSkillsFromCSV();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Print Skill Tree"))
        {
            SkillTreeBuilder treeBuilder = FindObjectOfType<SkillTreeBuilder>();
            if (treeBuilder != null)
            {
                treeBuilder.PrintSkillTree();
            }
            else
            {
                Debug.LogError("SkillTreeBuilder not found in the scene. Make sure it is attached to a GameObject.");
            }
        }
    }
}
