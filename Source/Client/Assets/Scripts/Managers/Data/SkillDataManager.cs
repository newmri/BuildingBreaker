using System.Collections.Generic;
using UnityCoreLibrary;
using UnityEngine;

public class SkillDataManager
{
    private Dictionary<int, Dictionary<string, object>> _skillList;

    public void Load()
    {
        CoreManagers.Data.LoadCSV("SkillID", "Data/Skill/Skill", out _skillList);
    }

    public Sprite GetSprite(int skillID)
    {
        return CoreManagers.Resource.Load<Sprite>(GetData(skillID, "ImagePath").ToString());
    }

    public float GetCoolTime(int skillID)
    {
        return (float)GetData(skillID, "CoolTime");
    }

    public string GetClassName(int skillID)
    {
        return (string)GetData(skillID, "ClassName");
    }

    public string GetAnimationName(int skillID)
    {
        return (string)GetData(skillID, "AnimationName");
    }

    public object GetData(int skillID, string name)
    {
        return _skillList[skillID][name];
    }
}
