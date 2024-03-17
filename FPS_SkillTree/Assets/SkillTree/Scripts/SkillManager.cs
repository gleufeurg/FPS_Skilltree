using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public string Name;
    public int Order;
    public float Cost;
    public string Type;
    public string Description;
    public List<SkillData> Children;

    public SkillData(string name, int order, float cost, string type, string description)
    {
        Name = name;
        Order = order;
        Cost = cost;
        Type = type;
        Description = description;
        Children = new List<SkillData>();
    }
}

public class SkillManager : MonoBehaviour
{
    public TextAsset skillDataCSV; // The CSV file to load
    private List<SkillData> skillList = new List<SkillData>();

    [HideInInspector] public string skillNameToSearch = "Test"; // The default skill name to search

    void Start()
    {
        LoadSkillsFromCSV();
    }

    public void LoadSkillsFromCSV()
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

    public SkillData FindSkillByName(string name)
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

    // Method to get all skills
    public List<SkillData> GetAllSkills()
    {
        return skillList;
    }
}
