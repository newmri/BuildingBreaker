using System.Collections;
using System.Collections.Generic;
using UnityCoreLibrary;
using UnityCoreLibrary.Animation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static WeaponSprite;

public class PlayerController : BaseController
{
    private Animator _animator;
    private SkillManager _skillManager;
    private Rigidbody2D _rigidbody;
    private WeaponSprite _weaponSprite;
    protected override void Init()
    {
        _skillManager = GetComponent<SkillManager>();
        _skillManager.Init();
        _skillManager.AddSkill(1);
        _skillManager.AddSkill(2);

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _weaponSprite = Util.FindChild<WeaponSprite>(gameObject);

        base.Init();
    }

    protected override bool UpdateController()
    {
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
                if("Attack" == _skillManager.AnimationName)
                    _weaponSprite.Attack = true;

                _animator.SetBool(_skillManager.AnimationName, true, (string name) =>
                {
                    _animator.SetBool(name, false);

                    State = ObjectState.IDLE;
                    _weaponSprite.Attack = false;
                });
                break;
        }
    }

    protected override void UpdateIdle()
    {
        base.UpdateIdle();
    }

    protected virtual void UpdateSkill()
    {

    }

    public void UseSkill(byte skillID)
    {
        if (false == _skillManager.CanUseSkill(skillID))
            return;

        _skillManager.UseSkill(skillID);

        State = ObjectState.SKILL;
    }

    public void Jump(float jumpPower)
    {
        _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.name)
        {
            case "Ground":
                State = ObjectState.IDLE;
                _skillManager.GetSkill<JumpSkill>(nameof(JumpSkill)).JumpCount = 0;
                break;
        }
    }

    public bool IsFalling()
    {
        return 0.0f > _rigidbody.velocity.y;
    }
}
