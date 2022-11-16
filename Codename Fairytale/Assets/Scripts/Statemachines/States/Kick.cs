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

    //so the animation of kick is smooth
    private float _changeTime;

    //used to keep from calling corutine function multiple times
    private bool _readyStart;

    public Kick(BossSM stateMachine) : base("Kick", stateMachine)
    {
        _bsm = (BossSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _changeTime = 1.9f - _bsm.waitTime;
        _cankick = true;
        _readyStart = true;
        //TODO: play kick animation
        _bsm.DoAnimations(3);
        Debug.Log("kick");
    }

    public override void UpdateLogic()
    {
        if (_bsm.IsStunned)
        {
            Debug.Log("stunned stomp");
            stateMachine.ChangeState(_bsm.idleState);
        }

        base.UpdateLogic();

        if (_bsm.isFacingPlayer)
        {
            FlipForKick();
            
        }
        //kick a projectile so change to shootState
        //if not shoot state go to idle state
        if (!_cankick && _readyStart)
        {
            _bsm.StartCoroutine(StartChangingState());
        }
    }

    public override void UpdatePhysics()
    {
        if (_bsm.IsStunned) return;

        base.UpdatePhysics();
        //check if isfacingplayer is true and within the radius to kick them
        //call function to damage player or ask aissa how that works so you can
        //see how to work with your code
        if (_cankick)
        {
            _cankick = false;
        }
        Attack();
        
    }

    private void Attack()
    {
        if (_bsm.target != null)
        {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_bsm.kickPoint.position, _bsm.kickRange, _bsm.targetLayers);

            // damage them
            foreach (Collider2D hit in enemiesHit)
            {
                if (hit.gameObject.TryGetComponent(out PlayerHealth target))
                {
                    if (!target.WasJustDamaged)
                    {
                        target.TakeDamage(_bsm.kickDamage);
                    }
                }
            }
        }
    }

    public IEnumerator StartChangingState()
    {
        if (_readyStart)
        {
            _readyStart = false;
            yield return _bsm.StartCoroutine(ChangingState());
        }
    }

    public IEnumerator ChangingState()
    {
        _bsm.numCharges = _bsm.ogChargeNum;
        yield return new WaitForSeconds(_bsm.waitTime + _changeTime);
        stateMachine.ChangeState(_bsm.idleState);
    }
    
    //flips character so that they can do back kick
    private void FlipForKick()
    {
        if (_bsm.target != null)
            {
                Vector2 scale = _bsm.transform.localScale;
                //if boss facing left but player on right side this fixes that and vice versa
                if (_bsm.target.transform.position.x < _bsm.transform.position.x)
                {
                    scale.x = Mathf.Abs(scale.x) * -1 * (_bsm.flip ? -1 : 1);
                }
                else 
                {
                    scale.x = Mathf.Abs(scale.x) * (_bsm.flip ? -1 : 1);
                }
                _bsm.transform.localScale = scale;
                _bsm.isFacingPlayer = false;
            }
    }
}
