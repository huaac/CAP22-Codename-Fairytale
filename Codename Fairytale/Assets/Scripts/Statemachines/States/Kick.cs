using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : BaseState
{
    private BossSM _bsm;

    public Kick(BossSM stateMachine) : base("Kick", stateMachine)
    {
        _bsm = (BossSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //kick a projectile so change to shootState
        //if not shoot state go to idle state
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        //check if isfacingplayer is true and within the radius to kick them
        //call function to damage player or ask aissa how that works so you can
        //see how to work with your code
    }
}
