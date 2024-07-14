using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillManager : MonoBehaviour
{
    private Dictionary<byte, Skill> _skillList = new Dictionary<byte, Skill>();
    private UIGameScene _uiGameScene;

    public string AnimationName { get; private set; }

    public void Init()
    {
        _uiGameScene = Managers.UI.GetSceneUI<UIGameScene>();
    }

    private void Update()
    {
        var time = Time.time;
        foreach (var skill in _skillList.Values)
        {
            if (false == skill.UsedSkill)
                continue;

            skill.ElapseCoolTime(time);
            _uiGameScene.UpdateSkillCoolTime(skill.SkillID, skill.ElapsedCoolTime / skill.CoolTime);
        }
    }

    public void AddSkill(byte skillID)
    {
        _skillList.Add(skillID, CreateSkill(skillID));
        _uiGameScene.AddSkill(skillID);
    }

    private Skill CreateSkill(byte skillID)
    {
        var type = Type.GetType(Managers.SkillData.GetClassName(skillID));
        return (Skill)Activator.CreateInstance(type, skillID);
    }

    public bool CanUseSkill(byte skillID)
    {
        return _skillList[skillID].CanUseSkill();
    }

    public void UseSkill(byte skillID)
    {
        AnimationName = Managers.SkillData.GetAnimationName(skillID);
        _skillList[skillID].UseSkill();
    }
}
