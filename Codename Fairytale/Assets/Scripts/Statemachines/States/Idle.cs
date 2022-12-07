using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private BossSM _bsm;
    private bool _readyStart;

    public Idle(BossSM stateMachine) : base("Idle", stateMachine)
    {
        _bsm = (BossSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        
        //starting the state with the enemy not moving
        _bsm.rb.velocity = Vector2.zero;
        //fixing rotation
        Vector3 eulerRotation = _bsm.transform.rotation.eulerAngles;
        _bsm.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        //automatically say the enemy not facing player to correct it
        _bsm.isFacingPlayer = false;
        _readyStart = true;

        Debug.Log("idle");
        //playing idle animation
        _bsm.DoAnimations(0);

    }

    public override void UpdateLogic()
    {
        if (!_bsm.fightHasStarted) return;  // temporary line of code to keep goat from going into states before battle has started

        if (_bsm.target == null) return;

        base.UpdateLogic();
        //once enemy is facing player change state
        if (_bsm.isFacingPlayer && _readyStart)
        {
            _readyStart = false;
            _bsm.StartCoroutine(StartChangingState());
        }
    }

    public override void UpdatePhysics()
    {
        if (!_bsm.fightHasStarted) return;  // temporary line of code to keep goat from going into states before battle has started

        base.UpdatePhysics();
        if (_bsm.IsStunned) return;

        //face player
        Vector2 scale = _bsm.transform.localScale;
        if (_bsm.target != null)
        {
            //if boss facing left but player on right side this fixes that and vice versa
            if (_bsm.target.transform.position.x > _bsm.transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (_bsm.flip ? -1 : 1);
                //facing "right"
                _bsm.facingRight = true;
            }
            else 
            {
                scale.x = Mathf.Abs(scale.x) * (_bsm.flip ? -1 : 1);
                //facing "left"
                _bsm.facingRight = false;
            }
            //fixes the chargeSpeed so that is will be charging the right way
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

    public IEnumerator StartChangingState()
    {
        yield return _bsm.StartCoroutine(ChangingState());
    }

    public IEnumerator ChangingState()
    {
        yield return new WaitForSeconds(_bsm.waitTime);
        stateMachine.ChangeState(_bsm.chargeState);
    }
}
