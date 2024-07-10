using UnityCoreLibrary.Animation;
using UnityEngine;

public class PlayerController : BaseController
{
    private Animator _animator;

    protected override void Init()
    {
        _animator = GetComponent<Animator>();
        base.Init();
    }

    protected override bool UpdateController()
    {
        GetInput();

        if (base.UpdateController())
            return true;

        switch (State)
        {
            case ObjectState.ATTACK:
                UpdateAttack();
                return true;
        }

        return false;
    }

    protected override void UpdateAnimation()
    {
        switch (State)
        {
            case ObjectState.ATTACK:
                _animator.SetBool("OnAttack", true, () =>
                {
                    _animator.SetBool("OnAttack", false);
                    State = ObjectState.IDLE;
                });
                break;
        }


    }

    protected virtual void UpdateAttack()
    {

    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            State = ObjectState.ATTACK;
        }
    }
}
