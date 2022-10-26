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
    }

    public override void UpdateLogic()
    {
        if (_bsm.IsStunned) return;

        base.UpdateLogic();
        if (_bsm.target != null)
        {
            Vector3 targetlocation = _bsm.target.transform.position;
            targetlocation = new Vector3(targetlocation.x, 6, targetlocation.z);
            if (_bsm.rock != null)
            {
                GameObject spawnedRock = UnityEngine.Object.Instantiate(_bsm.rock, targetlocation, Quaternion.identity);

            }
            stateMachine.ChangeState(_bsm.idleState);
        }
        else
        {
            stateMachine.ChangeState(_bsm.idleState);
        }
    }

    public override void UpdatePhysics()
    {
        if (_bsm.IsStunned) return;
        base.UpdatePhysics();
    }
}
