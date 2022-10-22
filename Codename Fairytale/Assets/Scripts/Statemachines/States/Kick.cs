// kicking state just happens
// is boss collides with player player takes damage
// there is a random generator that 1/3 would use shoot state
// or 2/3 idle state

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : BaseState
{
    private BossSM _bsm;
    private bool _cankick;
    private int _genRand;
    


    public Kick(BossSM stateMachine) : base("Kick", stateMachine)
    {
        _bsm = (BossSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _cankick = true;
        _genRand= Random.Range(1,3);
        Debug.Log(_genRand);
        //TODO: play kick animation
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //kick a projectile so change to shootState
        //if not shoot state go to idle state
        if (!_cankick)
        {
            _bsm.StartCoroutine(ChangingState());
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        //check if isfacingplayer is true and within the radius to kick them
        //call function to damage player or ask aissa how that works so you can
        //see how to work with your code
        if (_cankick)
        {
            Attack();
            _cankick = false;
        }

        
    }

    private void Attack()
    {
        if (_bsm.target != null)
        {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_bsm.kickPoint.position, _bsm.kickRange, _bsm.targetLayers);

            // damage them
            foreach (Collider2D hit in enemiesHit)
            {
                if (hit.gameObject.TryGetComponent(out PlayerMovement target))
                {
                    target.TakeDamage(_bsm.kickDamage);
                }
            }
        }
    }

     public IEnumerator ChangingState()
    {
        yield return new WaitForSeconds(_bsm.waitTime);
        if (_genRand == 1 && _bsm.target != null)
        {
            stateMachine.ChangeState(_bsm.shootState);
        }
        else
        {
            stateMachine.ChangeState(_bsm.idleState);

        }
    }
}
