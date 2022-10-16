using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : BaseState
{
    private BossSM _bsm;

    public Charge(BossSM stateMachine) : base("Charge", stateMachine)
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
        //change state here back to Idle when reached targeted position 
        //or facing away from player and player is far away
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        //charge at set position
        //stop
        //set state finsihed true
    }

}
