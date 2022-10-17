using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private BossSM _bsm;

    public Idle(BossSM stateMachine) : base("Idle", stateMachine)
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
        //change state here
        //make random choice between attacks 
        //each attack has its own cooldown
        //if facing player charge/kick/projectile and check a cooldown if you can do that state
        //example -> if (something) {stateMachine.ChangeState(_bsm.idleState)}
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        //face player
        Vector2 scale = _bsm.transform.localScale;
        if (_bsm.target != null)
        {
            if (_bsm.target.transform.position.x > _bsm.transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (_bsm.flip ? -1 : 1);
            }
            else 
            {
                scale.x = Mathf.Abs(scale.x) * (_bsm.flip ? -1 : 1);
            }
            _bsm.facingRight = !_bsm.facingRight;
            if (_bsm.facingRight)
            {
                _bsm.chargeSpeed = Mathf.Abs(_bsm.chargeSpeed);
            }
            else
            {
                _bsm.chargeSpeed = -1 * Mathf.Abs(_bsm.chargeSpeed);
            }
        }
        

        _bsm.transform.localScale = scale;

    }
}
