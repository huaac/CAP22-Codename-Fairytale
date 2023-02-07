using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNCCharge : BaseState
{
    private PNCSM _pncSM;
    private bool _readyStart;

    public PNCCharge(PNCSM stateMachine) : base("Charge", stateMachine)
    {
        _pncSM = (PNCSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("c");

        // after a wait time the state will change to patrol/charge
        _pncSM.GetComponent<ChargeEnemyAI>().isIdle = false;
        _pncSM.GetComponent<ChargeEnemyAI>().isPatroling = false;
        _pncSM.GetComponent<ChargeEnemyAI>().isChargeing = true;
        _pncSM.currentState = 2;
        _readyStart = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_pncSM.IsStunned)
        {
            Debug.Log("stunned charge");
            stateMachine.ChangeState(_pncSM.idleState);
        }

        if (_pncSM.target != null)
        {
            Debug.Log(_pncSM.IsStunned);
            //get the distance  from enemy to player
            Vector2 toOther = _pncSM.transform.position - _pncSM.target.transform.position;
            _pncSM.distToPlayer = toOther.sqrMagnitude;

            if (!(_pncSM.distToPlayer <= _pncSM.radiusLength * _pncSM.radiusLength))
            {
                if(_readyStart)
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

    public IEnumerator StartChangingState()
    {
        yield return new WaitForSeconds(_pncSM.waitTime);
        _pncSM.GetComponent<ChargeEnemyAI>().isChargeing = false;
        _pncSM.GetComponent<ChargeEnemyAI>().isIdle = true;
        stateMachine.ChangeState(_pncSM.idleState);
    }

}
