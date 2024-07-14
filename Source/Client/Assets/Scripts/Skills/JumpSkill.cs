using UnityEngine;

public class JumpSkill : Skill
{
    public JumpSkill(byte skillID) : base(skillID)
    {
        
    }

    public override void UseSkill()
    {
        base.UseSkill();
        Managers.Object.Player.Jump();  
    }
}
