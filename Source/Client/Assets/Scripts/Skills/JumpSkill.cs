using UnityEngine;

public class JumpSkill : Skill
{
    private float _jumpPower = 7.0f;
    private byte _maxJumpCount = 2;
    public byte JumpCount {  get; set; } 

    public JumpSkill(byte skillID) : base(skillID)
    {
        
    }

    public override bool CanUseSkill()
    {
        if(false == base.CanUseSkill())
            return false;

        return JumpCount < _maxJumpCount;
    }

    public override void UseSkill()
    {
        base.UseSkill();
        ++JumpCount;

        Managers.Object.Player.Jump(_jumpPower);  
    }
}
