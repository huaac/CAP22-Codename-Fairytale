using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNTIdle : BaseState
{
    private CNoTSM _cnotSM;
    private bool _readyStart;

     public CNTIdle(CNoTSM stateMachine) : base("Idle", stateMachine)
    {
        _cnotSM = (CNoTSM)stateMachine;
    }

    public override void Enter()
    {
        _readyStart = true;
        // after player comes within range the state will change to charge
        _cnotSM.GetComponent<ChargeEnemyAI>().isIdle = true;
        _cnotSM.GetComponent<ChargeEnemyAI>().isPatroling = false;
        _cnotSM.GetComponent<ChargeEnemyAI>().isChargeing = false;
        _cnotSM.DoAnimations(0);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_cnotSM.target != null)
        {
            //get the distance from enemy to player
            Vector2 toOther = _cnotSM.transform.position - _cnotSM.target.transform.position;
            _cnotSM.distToPlayer = toOther.sqrMagnitude;

            //if target is within range
            if (_cnotSM.distToPlayer <= _cnotSM.radiusLength * _cnotSM.radiusLength)
            {
                if (_readyStart)
                {
                    _readyStart = false;
                    _cnotSM.GetComponent<ChargeEnemyAI>().isIdle = false;
                    _cnotSM.GetComponent<ChargeEnemyAI>().isPatroling = false;
                    _cnotSM.GetComponent<ChargeEnemyAI>().isChargeing = true;
                    stateMachine.ChangeState(_cnotSM.chargeState);
                }

            }

        }
        
    }


}
