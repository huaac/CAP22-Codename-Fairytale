using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNTCharge : BaseState
{
    private CNoTSM _cnotSM;
    private bool _readyStart;

    public CNTCharge(CNoTSM stateMachine) : base("Charge", stateMachine)
    {
        _cnotSM = (CNoTSM)stateMachine;
    }
    
    public override void Enter()
    {
        _readyStart = true;
        // after player comes within range the state will change to charge
        _cnotSM.GetComponent<ChargeEnemyAI>().isIdle = false;
        _cnotSM.GetComponent<ChargeEnemyAI>().isPatroling = false;
        _cnotSM.GetComponent<ChargeEnemyAI>().isChargeing = true;
        _cnotSM.DoAnimations(1);

        _cnotSM.StartCoroutine(StartLifeSpan());
    }

    public IEnumerator StartLifeSpan()
    {
        yield return new WaitForSeconds(_cnotSM.lifeSpan);
        _cnotSM.DestroyOb();
    }


}
