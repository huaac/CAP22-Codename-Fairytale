using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : BaseState
{
    private BossSM _bsm;
    private float _distToPlayer;
    private int ogChargeNum;
    
    

    public Charge(BossSM stateMachine) : base("Charge", stateMachine)
    {
        _bsm = (BossSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        //TODO: play charge animation
        _bsm.isFacingPlayer = true;
    }

    public override void UpdateLogic()
    {
        if (_bsm.IsStunned) return;

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
                if(Vector2.Dot(_bsm.transform.TransformDirection(Vector3.left), toOther) > 0)
                {
                    _bsm.isFacingPlayer = false;
                }
            }
            else
            {
                if(Vector2.Dot(_bsm.transform.TransformDirection(Vector3.left), toOther) < 0)
                {
                    _bsm.isFacingPlayer = false;
                }
            }
            
            
            if (_distToPlayer < _bsm.radiusLength && _bsm.isFacingPlayer && _bsm.numCharges <= 0)
            {
                Debug.Log("kick state");
                _bsm.numCharges = _bsm.ogChargeNum;
                stateMachine.ChangeState(_bsm.kickState);
            }
            else if (_distToPlayer > _bsm.radiusLength && !_bsm.isFacingPlayer)
            {
                Debug.Log("idle state");
                _bsm.numCharges -= 1;
                stateMachine.ChangeState(_bsm.idleState);
            }
            
        }
        else
        {
            _bsm.numCharges -= 1;
            stateMachine.ChangeState(_bsm.idleState);

        }
        
    }

    public override void UpdatePhysics()
    {
        if (_bsm.IsStunned) return;

        base.UpdatePhysics();
        
        _bsm.rb.velocity = new Vector2(_bsm.chargeSpeed * Time.fixedDeltaTime, _bsm.rb.velocity.y);
        Vector3 eulerRotation = _bsm.transform.rotation.eulerAngles;
        _bsm.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        Attack();

    }


    private void Attack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_bsm.kickPoint.position, (_bsm.kickRange-0.9f), _bsm.targetLayers);

        foreach (Collider2D hit in enemiesHit)
        {
            if (hit.gameObject.TryGetComponent(out PlayerMovement target))
            {
                if (!target.WasJustDamaged)
                {
                    target.TakeDamage(_bsm.chargeDamage);
                }
            }
        }

    }

}
