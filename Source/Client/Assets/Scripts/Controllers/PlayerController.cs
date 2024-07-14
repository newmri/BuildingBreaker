using System.Collections.Generic;
using UnityCoreLibrary.Animation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : BaseController
{
    private Animator _animator;
    private SkillManager _skillManager;
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float _jumpPower = 5.0f;

    protected override void Init()
    {
        _skillManager = GetComponent<SkillManager>();
        _skillManager.Init();
        _skillManager.AddSkill(1);
        _skillManager.AddSkill(2);

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
                _animator.SetBool(_skillManager.AnimationName, true, (string name) =>
                {
                    _animator.SetBool(name, false);
                    State = ObjectState.IDLE;
                });
                break;
        }
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

    public void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.name)
        {
            case "Ground":
                State = ObjectState.IDLE;
                break;
        }
    }
}
