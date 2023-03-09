using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Patrol and Charge enemy
public class PNCSM : StateMachine
{
    //states variables

    [HideInInspector]
    //idle state the enemy has no movement other than facing player
    public PNCIdle idleState;
    [HideInInspector]
    //patrol state the enemy has movement around given area
    public PNCPatrol patrolState;

    [HideInInspector]
    public PNCCharge chargeState;

    //used to see what was the state before helping with going from idle to another state
    // 1 = patrol, 2 = charge
    [HideInInspector]
    public int currentState;

    [HideInInspector]
    public GameObject target;

    //rigidbody used for any movement of enemy
    [HideInInspector]
    public Rigidbody2D rb;

    [HideInInspector]
    public float distToPlayer;

    [HideInInspector]
    public ChargeEnemyAI chargeEnemyAI;
    
    [Header("General Use")]
    //wait time to move to next state
    public float waitTime;
    //radius of boss to help check player position
    public float radiusLength;
    public Animator m_anim;

   private void Awake() 
    {
        idleState = new PNCIdle(this);
        patrolState = new PNCPatrol(this);
        chargeState = new PNCCharge(this);
        rb = GetComponent<ChargeEnemyAI>().rb;
        currentState = 1; //initiating at patrol state
        target = GetComponent<ChargeEnemyAI>().target;
        chargeEnemyAI = GetComponent<ChargeEnemyAI>();
        m_anim.SetInteger("currentState", 0); //enemy patrol

    }

    protected override BaseState GetInitialState()
    {
        return patrolState;
    }

    public void DoAnimations(int num)
    {
        m_anim.SetInteger("currentState", num);
    }
}
