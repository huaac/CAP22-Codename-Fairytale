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
        //access BossSM script public functions, variables
        _bsm = (BossSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        //TODO: play charge animation
        _bsm.isFacingPlayer = true;

        //check if not stunned if it is don't do charge state
        _bsm.DoAnimations(1);
    }

    public override void UpdateLogic()
    {
        if (_bsm.IsStunned)
        {
            Debug.Log("stunned charge");
            stateMachine.ChangeState(_bsm.idleState);
        }
        base.UpdateLogic();
        //change state here back to Idle when reached targeted position 
        //or facing away from player and player is far away

        //if player is not destroyed
        if (_bsm.target != null)
        {
            //get the distance  from enemy to player
            _distToPlayer = Vector2.Distance(_bsm.transform.position, _bsm.target.transform.position);
            Vector2 toOther = _bsm.transform.position - _bsm.target.transform.position;
            if (_bsm.facingRight)
            {
                //check if player is no longer facing right
                if(Vector2.Dot(_bsm.transform.TransformDirection(Vector3.left), toOther) < 0)
                {
                    _bsm.isFacingPlayer = false;
                }
            }
            else
            {
                //check if player is no longer facing left
                if(Vector2.Dot(_bsm.transform.TransformDirection(Vector3.left), toOther) > 0)
                {
                    _bsm.isFacingPlayer = false;
                }
            }
            // if player is close enough to kick and enemy is facing player then do the kick state and reset number of charges
            if (_distToPlayer < _bsm.radiusLength && _bsm.isFacingPlayer && _bsm.numCharges <= 0)
            {
                stateMachine.ChangeState(_bsm.kickState);
            }
            else if (_distToPlayer > _bsm.radiusLength && _bsm.numCharges <= 0)
            {
                stateMachine.ChangeState(_bsm.shootState);
            }
            //else if the player is too far from the enemy and enemy is not facing player then change to idle state and take away from number of charges
            else if (_distToPlayer > _bsm.radiusLength && !_bsm.isFacingPlayer)
            {
                _bsm.numCharges -= 1;
                //stateMachine.ChangeState(_bsm.kickState);
                stateMachine.ChangeState(_bsm.idleState);
            }
            
        }
        //if player is destroyed (defeated) then go to idle state and take away from charge
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
        
        // charges at player from set charge speed
        _bsm.rb.velocity = new Vector2(_bsm.chargeSpeed * Time.fixedDeltaTime, _bsm.rb.velocity.y);

        //fix rotation of enemy in case player bumps into boss
        Vector3 eulerRotation = _bsm.transform.rotation.eulerAngles;
        _bsm.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        Attack();

    }

    //attacks player only when they were not just attacked
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
