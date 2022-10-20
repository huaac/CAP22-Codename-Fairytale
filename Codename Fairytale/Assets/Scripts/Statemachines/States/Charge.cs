using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : BaseState
{
    private BossSM _bsm;
    private float _distToPlayer;
    
    

    public Charge(BossSM stateMachine) : base("Charge", stateMachine)
    {
        _bsm = (BossSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _bsm.isFacingPlayer = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //change state here back to Idle when reached targeted position 
        //or facing away from player and player is far away
        if (_bsm.target != null)
        {
            // if(Mathf.Approximately(differenceDot, 0.0f))
            //print("= 0");
            _distToPlayer = Vector2.Distance(_bsm.transform.position, _bsm.target.transform.position);
            Vector2 toOther = _bsm.transform.position - _bsm.target.transform.position;
            if (_bsm.facingRight)
            {
                if(Vector2.Dot(_bsm.transform.TransformDirection(Vector3.right), toOther) > 0)
                {
                    _bsm.isFacingPlayer = false;
                    Debug.Log("not facing player right");
                }
            }
            else
            {
                if(Vector2.Dot(_bsm.transform.TransformDirection(Vector3.left), toOther) < 0)
                {
                    _bsm.isFacingPlayer = false;
                    Debug.Log("not facing player left");
                }
            }
            
            if (_distToPlayer > _bsm.radiusLength && !_bsm.isFacingPlayer)
            {
                Debug.Log("change state");
                stateMachine.ChangeState(_bsm.idleState);
            }
        }
        else
        {
            stateMachine.ChangeState(_bsm.idleState);

        }
        
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        
        _bsm.rb.velocity = new Vector2(_bsm.chargeSpeed * Time.fixedDeltaTime, _bsm.rb.velocity.y);

    }
    

}
