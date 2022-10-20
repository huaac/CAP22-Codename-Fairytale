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
        _bsm.rb.velocity = Vector2.zero;
         Vector3 eulerRotation = _bsm.transform.rotation.eulerAngles;
        _bsm.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        _bsm.isFacingPlayer = false;
    }

    public override void UpdateLogic()
    {
        if (_bsm.target == null) return;

        base.UpdateLogic();
        if (_bsm.isFacingPlayer)
        {
            stateMachine.ChangeState(_bsm.chargeState);
        }
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
                Debug.Log("right");
                _bsm.facingRight = true;
            }
            else 
            {
                scale.x = Mathf.Abs(scale.x) * (_bsm.flip ? -1 : 1);
                Debug.Log("left");
                _bsm.facingRight = false;
            }
            
            if (_bsm.facingRight)
            {
                _bsm.chargeSpeed = Mathf.Abs(_bsm.chargeSpeed);
            }
            else
            {
                _bsm.chargeSpeed = -1 * Mathf.Abs(_bsm.chargeSpeed);
            }
        }
        _bsm.isFacingPlayer = true;
        

        _bsm.transform.localScale = scale;

    }
}
