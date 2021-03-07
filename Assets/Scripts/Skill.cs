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
}

[System.Serializable]
public enum SkillType
{
    NONE,
    ATTACK
}