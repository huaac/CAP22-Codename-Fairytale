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
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
