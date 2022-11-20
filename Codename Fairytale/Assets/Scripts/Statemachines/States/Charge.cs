using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : BaseState
{
    private BossSM _bsm;
    private float _distToPlayer;
    private int ogChargeNum;
    private bool _readyStart;
    

    public Charge(BossSM stateMachine) : base("Charge", stateMachine)
    {
        //access BossSM script public functions, variables
        _bsm = (BossSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _bsm.isFacingPlayer = true;
        _readyStart = true;

        //play charge animation
        _bsm.DoAnimations(1);
        Debug.Log("charge");
    }

    public override void UpdateLogic()
    {
        //note should stunned be moved to the bottom since it comes to fast (shoot)
        if (_bsm.IsStunned)
        {
            Debug.Log("stunned charge");
            _bsm.numCharges -= 1;
            stateMachine.ChangeState(_bsm.idleState);
        }
        base.UpdateLogic();
        //change state here back to Idle when reached targeted position 
        //or facing away from player and player is far away

        //if player is not destroyed
        if (_bsm.target != null)
        {
            //get the distance  from enemy to player
            Vector2 toOther = _bsm.transform.position - _bsm.target.transform.position;
            //_distToPlayer = Vector2.Distance(_bsm.transform.position, _bsm.target.transform.position);
            _distToPlayer = toOther.sqrMagnitude;
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

            if (_readyStart)
            {
                // if player is close enough to kick and enemy is facing player then do the kick state and reset number of charges
                if (_bsm.numCharges <= 0 && _bsm.isFacingPlayer)
                {
                    //if target is within range
                    if (_distToPlayer <= _bsm.radiusLength * _bsm.radiusLength)
                    {
                        _readyStart = false;
                        _bsm.StartCoroutine(StartChangingState(1));
                    }
                    //if target is not within range
                    else if (!(_distToPlayer < _bsm.radiusLength * _bsm.radiusLength))
                    {
                        _readyStart = false;
                        _bsm.StartCoroutine(StartChangingState(2));
                    }
                }
                //else if the player is too far from the enemy and enemy is not facing player then change to idle state and take away from number of charges
                else if (!_bsm.isFacingPlayer)
                {
                    //if target is not within range
                    if (!(_distToPlayer < (_bsm.radiusLength - 0.5) *(_bsm.radiusLength - 0.5)))
                    {
                        _readyStart = false;
                        _bsm.numCharges -= 1;
                        _bsm.StartCoroutine(StartChangingState(0));
                    }
                }
            }
            
            
        }
        //if player is destroyed (defeated) then go to idle state and take away from charge
        else
        {
            _bsm.numCharges -= 1;
            _bsm.StartCoroutine(StartChangingState(0));

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

    }

    public IEnumerator StartChangingState(int state)
    {
        if (state == 0)
            yield return _bsm.StartCoroutine(ChangeToIdleState());
        else if (state == 1)
            yield return _bsm.StartCoroutine(ChangeToKickState());
        else if (state == 2)
            yield return _bsm.StartCoroutine(ChangeToShootState());
    }
    public IEnumerator ChangeToIdleState()
    {
        yield return new WaitForSeconds(_bsm.waitTime);
        stateMachine.ChangeState(_bsm.idleState);
    }
    public IEnumerator ChangeToKickState()
    {
        yield return new WaitForSeconds(_bsm.waitTime);
        stateMachine.ChangeState(_bsm.kickState);
    }
    public IEnumerator ChangeToShootState()
    {
        yield return new WaitForSeconds(_bsm.waitTime);
        stateMachine.ChangeState(_bsm.shootState);
    }

}
