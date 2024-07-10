using UnityEngine;

public enum ObjectState : byte
{
    IDLE = 0,
    ATTACK,
};

public class BaseController : MonoBehaviour
{
    ObjectState _state = ObjectState.IDLE;

    public virtual ObjectState State
    {
        get { return _state; }
        set { _state = value; UpdateAnimation(); }
    }

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        UpdateAnimation();
    }

    protected virtual void Update()
    {
        UpdateController();
    }

    protected virtual bool UpdateController()
    {
        switch (State)
        {
            case ObjectState.IDLE:
                UpdateIdle();
                return true;
        }

        return false;
    }

    protected virtual void UpdateAnimation()
    {

    }

    protected virtual void UpdateIdle()
    {
    }
}
