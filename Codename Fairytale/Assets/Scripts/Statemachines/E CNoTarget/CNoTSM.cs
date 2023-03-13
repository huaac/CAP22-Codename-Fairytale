using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNoTSM : StateMachine, IEnemy
{
    //states variables

    [HideInInspector]
    //idle state the enemy has no movement other than waiting for to get close enough player
    public CNTIdle idleState;

    [HideInInspector]
    //charge until its life span ends
    public CNTCharge chargeState;

    [HideInInspector]
    public GameObject target;

    [HideInInspector]
    public float distToPlayer;

    [HideInInspector]
    public ChargeEnemyAI chargeEnemyAI;

    public bool IsStunned { get; set; }

    [Header("General Use")]
    //how long until it dies/destroyed
    public float lifeSpan;
    //radius of enemy to help check player position
    public float radiusLength;
    public Animator m_anim;

    private void Awake()
    {
        idleState = new CNTIdle(this);
        chargeState = new CNTCharge(this);
        target = GetComponent<ChargeEnemyAI>().target;
        chargeEnemyAI = GetComponent<ChargeEnemyAI>();
        m_anim.SetInteger("currentState", 0); //enemy idle
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    public void DoAnimations(int num)
    {
        m_anim.SetInteger("currentState", num);
    }

    public void DestroyOb()
    {
        Debug.Log("started the charge");
        chargeEnemyAI.DestroyObject();
    }

}
