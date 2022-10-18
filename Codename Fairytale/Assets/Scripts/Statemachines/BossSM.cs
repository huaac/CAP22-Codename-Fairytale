using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSM : StateMachine
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




    //rigidbody used for any movement of enemy
    public Rigidbody2D rb;
    //target is player but can be anything really
    public GameObject target;
    //to correct the side the enemy flips too
    public bool flip;
    //radius of boss to help check player position
    public float _radiusLength;
    //as soon as charge state starts the position of player at that point is taken and used to charge
    [HideInInspector]
    public Vector3 targetLocation;
    public bool facingRight;
    


    //for the charge and run state
    public float chargeSpeed;
    //public float runSpeed;
    
    private void Awake() 
    {
        idleState = new Idle(this);
        chargeState = new Charge(this);
        kickState = new Kick(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}