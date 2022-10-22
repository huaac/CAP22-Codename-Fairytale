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
        base.UpdateLogic();
        if (_bsm.target != null)
        {
            Transform targetlocation = _bsm.target.transform;
            targetlocation.position = new Vector3(targetlocation.position.x, 6, targetlocation.position.z);
            rock = GameObject.Find("FallingRock");
            GameObject spawnedRock = UnityEngine.Object.Instantiate(rock, targetlocation.position, Quaternion.identity);
            stateMachine.ChangeState(_bsm.idleState);
        }
        else
        {
            stateMachine.ChangeState(_bsm.idleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
