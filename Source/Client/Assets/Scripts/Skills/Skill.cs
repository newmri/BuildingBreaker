using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Skill
{
    public Skill(byte skillID)
    {
        SkillID = skillID;
        CoolTime = Managers.SkillData.GetCoolTime(skillID);
    }

    public byte SkillID { get; private set; } = 0;
    public bool UsedSkill { get; private set; } = false;
    public float ElapsedCoolTime { get; private set; } = 0.0f;
    public float CoolTime { get; private set; } = 0.0f;

    public bool CanUseSkill()
    {
        return 0.0f == ElapsedCoolTime;
    }

    public void UseSkill()
    {
        UsedSkill = true;
    }

    public void ElapseCoolTime(float time)
    {
        ElapsedCoolTime += Time.deltaTime;
        if (ElapsedCoolTime >= CoolTime)
            Clear();
    }

    private void Clear()
    {
        UsedSkill = false;
        ElapsedCoolTime = 0.0f;
    }
}
