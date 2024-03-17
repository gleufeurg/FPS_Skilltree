using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillTreeBuilder))]
public class SkillTreeBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SkillTreeBuilder builder = (SkillTreeBuilder)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Print Skill Tree"))
        {
            builder.PrintSkillTree();
        }
    }
}
