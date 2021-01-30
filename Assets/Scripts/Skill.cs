using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public int skillCode;
    public string[] skillName;
    public int skillLevel;

    public SkillType skillType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public enum SkillType
{
    NONE,
    ATTACK
}