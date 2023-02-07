using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNCIdle : BaseState
{
    private PNCSM _pncSM;
    private bool _readyStart;


    public PNCIdle(PNCSM stateMachine) : base("Idle", stateMachine)
    {
        _pncSM = (PNCSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("i");

        //starting the state with the enemy not moving
        _pncSM.rb.velocity = Vector2.zero;

        //fixing rotation
        Vector3 eulerRotation = _pncSM.transform.rotation.eulerAngles;
        _pncSM.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);

        // after a wait time the state will change to patrol/charge
        _pncSM.GetComponent<ChargeEnemyAI>().isIdle = true;
        _pncSM.GetComponent<ChargeEnemyAI>().isPatroling = false;
        _pncSM.GetComponent<ChargeEnemyAI>().isChargeing = false;
        _readyStart = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_pncSM.target != null)
        {
            Debug.Log(_pncSM.IsStunned);
            if (!_pncSM.IsStunned && _readyStart)
            {
                _readyStart = false;
                _pncSM.StartCoroutine(StartChangingState());
            }
            else if (_pncSM.IsStunned)
            {
                Debug.Log("stunned idle");
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public IEnumerator StartChangingState()
    {
        yield return new WaitForSeconds(_pncSM.waitTime);
        _pncSM.GetComponent<ChargeEnemyAI>().isIdle = false;
        if (_pncSM.currentState == 1) //are going from patrol to charge
        {
            _pncSM.GetComponent<ChargeEnemyAI>().isChargeing = true;
            _pncSM.currentState = 2; // charge state
            stateMachine.ChangeState(_pncSM.chargeState);
        }
        else if (_pncSM.currentState == 2) //charge to patrol
        {
            _pncSM.GetComponent<ChargeEnemyAI>().isPatroling = true;
            _pncSM.currentState = 1;
            stateMachine.ChangeState(_pncSM.patrolState);
        }
        
        
    }
}
