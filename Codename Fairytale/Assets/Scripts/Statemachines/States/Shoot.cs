using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : BaseState
{
    private BossSM _bsm;
    private float _distToPlayer;

    public Shoot(BossSM stateMachine) : base("Shoot", stateMachine)
    {
        _bsm = (BossSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _bsm.targetLocation = _bsm.target.transform.position;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_bsm.target != null)
        {
            _distToPlayer = Vector2.Distance(_bsm.transform.position, _bsm.targetLocation);
            Vector2 toOther = _bsm.transform.position - _bsm.targetLocation;
            stateMachine.ChangeState(_bsm.idleState);
        }
        else
        {
            stateMachine.ChangeState(_bsm.idleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
