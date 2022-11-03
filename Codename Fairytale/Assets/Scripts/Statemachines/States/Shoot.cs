using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : BaseState
{
    private BossSM _bsm;
    [SerializeField]private GameObject rock;

    public Shoot(BossSM stateMachine) : base("Shoot", stateMachine)
    {
        _bsm = (BossSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _bsm.targetLocation = _bsm.target.transform.position;
        _bsm.DoAnimations(2);
        Debug.Log("shoot");
    }

    public override void UpdateLogic()
    {
        if (_bsm.IsStunned)
        {
            Debug.Log("stunned stomp");
            stateMachine.ChangeState(_bsm.idleState);
        }
        base.UpdateLogic();
        if (_bsm.target != null)
        {
            Vector3 targetlocation = _bsm.target.transform.position;
            targetlocation = new Vector3(targetlocation.x, 6, targetlocation.z);
            if (_bsm.rock != null)
            {
                GameObject spawnedRock = UnityEngine.Object.Instantiate(_bsm.rock, targetlocation, Quaternion.identity);

            }
             _bsm.StartCoroutine(StartChangingState());
            //stateMachine.ChangeState(_bsm.idleState);
        }
        else
        {
            _bsm.StartCoroutine(StartChangingState());
            //stateMachine.ChangeState(_bsm.idleState);
        }
    }

    public override void UpdatePhysics()
    {
        if (_bsm.IsStunned) return;
        base.UpdatePhysics();
    }

    public IEnumerator StartChangingState()
    {
        yield return _bsm.StartCoroutine(ChangingState());
    }

    public IEnumerator ChangingState()
    {
        _bsm.numCharges = _bsm.ogChargeNum;
        stateMachine.ChangeState(_bsm.idleState);
        yield return new WaitForSeconds(_bsm.waitTime);
    }
}
