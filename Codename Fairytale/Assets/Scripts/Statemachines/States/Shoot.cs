using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : BaseState
{
    private BossSM _bsm;
    [SerializeField]private GameObject rock;

    private bool _readyStart;
    private bool _alreadyShot;

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
        _readyStart = false;
        _alreadyShot = false;
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
            if (!_alreadyShot)
            {
                Vector3 targetlocation = _bsm.target.transform.position;
                targetlocation = new Vector3(targetlocation.x, 6, targetlocation.z);
                if (_bsm.rock != null)
                {
                    GameObject spawnedRock = UnityEngine.Object.Instantiate(_bsm.rock, targetlocation, Quaternion.identity);

                }
                _alreadyShot = true;
                _readyStart = true;
            }
            // if (_readyStart)
            // {
            //     _bsm.StartCoroutine(StartChangingState());
            // }
            //stateMachine.ChangeState(_bsm.idleState);
        }
        else if (_bsm.target == null)
        {
            _readyStart = true;
            // _bsm.StartCoroutine(StartChangingState());
            //stateMachine.ChangeState(_bsm.idleState);
        }

        if (_readyStart)
        {
            _readyStart = false;
            _bsm.StartCoroutine(StartChangingState());
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
        Debug.Log("going to idle");
        _bsm.numCharges = _bsm.ogChargeNum;
        yield return new WaitForSeconds(_bsm.waitTime + 0.7f);
        stateMachine.ChangeState(_bsm.idleState);
    }
}
