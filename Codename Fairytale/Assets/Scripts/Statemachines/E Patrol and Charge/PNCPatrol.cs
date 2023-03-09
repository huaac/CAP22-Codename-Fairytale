using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNCPatrol : BaseState
{
    private PNCSM _pncSM;
    private bool _readyStart;

    public PNCPatrol(PNCSM stateMachine) : base("Patrol", stateMachine)
    {
        _pncSM = (PNCSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("p");

        // after a wait time the state will change to patrol/charge
        _pncSM.GetComponent<ChargeEnemyAI>().isIdle = false;
        _pncSM.GetComponent<ChargeEnemyAI>().isPatroling = true;
        _pncSM.GetComponent<ChargeEnemyAI>().isChargeing = false;
        _pncSM.currentState = 1;
        _readyStart = true;

        _pncSM.DoAnimations(0); //patrol state animation
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_pncSM.chargeEnemyAI.IsStunned)
        {
            stateMachine.ChangeState(_pncSM.idleState);
        }
        
        if (_pncSM.target != null)
        {
            //get the distance  from enemy to player
            Vector2 toOther = _pncSM.transform.position - _pncSM.target.transform.position;
            _pncSM.distToPlayer = toOther.sqrMagnitude;

            //if target is within range
            if (_pncSM.distToPlayer <= _pncSM.radiusLength * _pncSM.radiusLength)
            {
                if (_readyStart)
                {
                    _readyStart = false;
                    _pncSM.StartCoroutine(StartChangingState());
                }
            }
        }
        else if (_pncSM.target == null)
        {
            stateMachine.ChangeState(_pncSM.idleState);
        }

    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public IEnumerator StartChangingState()
    {
        yield return new WaitForSeconds(_pncSM.waitTime);
        _pncSM.GetComponent<ChargeEnemyAI>().isPatroling = false;
        _pncSM.GetComponent<ChargeEnemyAI>().isIdle = true;
        stateMachine.ChangeState(_pncSM.idleState);
    }

}
