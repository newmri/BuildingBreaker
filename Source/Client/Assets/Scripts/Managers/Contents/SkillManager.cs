using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillManager : MonoBehaviour
{
    private Dictionary<byte, Skill> _skillList = new Dictionary<byte, Skill>();
    private Dictionary<string, Skill> _skillListByName = new Dictionary<string, Skill>();

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
        var name = Managers.SkillData.GetClassName(skillID);
        var type = Type.GetType(name);
        var skill = (Skill)Activator.CreateInstance(type, skillID);

        _skillList.Add(skillID, skill);
        _skillListByName.Add(name, skill);

        _uiGameScene.AddSkill(skillID);
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

    public T GetSkill<T>(string name) where T : Skill
    {
        return (T)_skillListByName[name];
    }
}
