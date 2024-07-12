using System.Collections.Generic;
using UnityCoreLibrary.Animation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : BaseController
{
    private Animator _animator;
    private SkillManager _skillManager;

    protected override void Init()
    {
        _skillManager = GetComponent<SkillManager>();
        _skillManager.Init();
        _skillManager.AddSkill(1);
        _skillManager.AddSkill(2);

        _animator = GetComponent<Animator>();
        base.Init();
    }

    protected override bool UpdateController()
    {
        GetInput();

        if (base.UpdateController())
            return true;

        var isUpdated = false;

        switch (State)
        {
            case ObjectState.SKILL:
                UpdateSkill();
                isUpdated = true;
                break;
        }


        return isUpdated;
    }

    protected override void UpdateAnimation()
    {
        switch (State)
        {
            case ObjectState.SKILL:
                _animator.SetBool("OnAttack", true, () =>
                {
                    _animator.SetBool("OnAttack", false);
                    State = ObjectState.IDLE;
                });
                break;
        }


    }

    protected virtual void UpdateSkill()
    {

    }

    private void GetInput()
    {
        
    }

    public void UseSkill(byte skillID)
    {
        if (State == ObjectState.SKILL || false == _skillManager.CanUseSkill(skillID))
            return;

        State = ObjectState.SKILL;

        _skillManager.UseSkill(skillID);
    }
}
