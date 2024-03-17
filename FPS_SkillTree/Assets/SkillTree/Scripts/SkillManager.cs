using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct SkillData
{
    public string Name;
    public int Order;
    public float Cost;
    public string Type;
    public string Description;

    public SkillData(string name, int order, float cost, string type, string description)
    {
        Name = name;
        Order = order;
        Cost = cost;
        Type = type;
        Description = description;
    }
}

public class SkillManager : MonoBehaviour
{
    public TextAsset skillDataCSV; // The CSV file to load
    private List<SkillData> skillList = new List<SkillData>();

    public bool printSkillInfo = true; // Control whether to print skill information
    public string skillNameToSearch = "Skill2"; // The default skill name to search

    void Start()
    {
        StartSkillManager();
    }

    public void StartSkillManager()
    {
        LoadSkillsFromCSV();

        // Example: Finding skill data by name
        SkillData foundSkill = FindSkillByName(skillNameToSearch);

        if (foundSkill.Name != null && printSkillInfo)
        {
            Debug.Log($"Skill found - Name: {foundSkill.Name}, Order: {foundSkill.Order}, Cost: {foundSkill.Cost}, Type: {foundSkill.Type}, Description: {foundSkill.Description}");
        }
        else if (!printSkillInfo)
        {
            Debug.Log("Skill information printing is disabled.");
        }
        else
        {
            Debug.Log($"Skill with name '{skillNameToSearch}' not found.");
        }
    }

    void LoadSkillsFromCSV()
    {
        if (skillDataCSV == null)
        {
            Debug.LogError("No skillDataCSV assigned in the inspector!");
            return;
        }

        string[] lines = skillDataCSV.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] data = SplitCSVLine(lines[i]);

            if (data.Length >= 5)
            {
                string name = data[0];
                int order = int.Parse(data[1]);
                float cost = float.Parse(data[2]);
                string type = data[3];
                string description = data[4];

                SkillData skill = new SkillData(name, order, cost, type, description);
                skillList.Add(skill);
            }
        }
    }

    SkillData FindSkillByName(string name)
    {
        return skillList.FirstOrDefault(skill => skill.Name == name);
    }

    string[] SplitCSVLine(string line)
    {
        List<string> fields = new List<string>();

        bool inQuotes = false;
        string field = "";

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(field);
                field = "";
            }
            else
            {
                field += c;
            }
        }

        fields.Add(field); // Add the last field

        return fields.ToArray();
    }
}
