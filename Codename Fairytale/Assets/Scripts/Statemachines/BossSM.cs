using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSM : StateMachine, IEnemy
{
    //states variables

    [HideInInspector]
    //idle state the enemy has no movement other than facing player
    public Idle idleState;

    [HideInInspector]
    //charge state the enemy moves towards player (more detail on Charge stript)
    public Charge chargeState;

    [HideInInspector]
    public Kick kickState;

    [HideInInspector]
    //shoot state the enemy shoots a projectile at the player
    public Shoot shootState;

    [Header("Attack Pattern")]
    public int numCharges;
    public float waitTime;

    public bool IsStunned { get; set; }

    //as soon as charge state starts the position of player at that point is taken and used to charge
    [HideInInspector]
    public Vector3 targetLocation;
    [HideInInspector]
    public bool isFacingPlayer;
    
    [Header("General Use")]
    //rigidbody used for any movement of enemy
    public Rigidbody2D rb;
    //target is player but can be anything really
    public GameObject target;
    public bool facingRight;
    //to correct the side the enemy flips too
    public bool flip;

    [Header("Charge State")]
    //radius of boss to help check player position
    public float radiusLength;
    //for the charge
    public float chargeSpeed;
    public int chargeDamage;
    [HideInInspector]
    public int ogChargeNum;

    [Header("Kick State")]
    public Transform kickPoint;
    //used to see if player was within the of kicking
    public float kickRange;
    public LayerMask targetLayers;
    public int kickDamage;

    [Header("Shoot State")]
    public GameObject rock;


    

    
    private void Awake() 
    {
        idleState = new Idle(this);
        chargeState = new Charge(this);
        kickState = new Kick(this);
        shootState = new Shoot(this);

        ogChargeNum = numCharges;

    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}