using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSM : StateMachine
{
    //states variables
    [HideInInspector]
    //idle state the enemy has no movement other than facing player
    public Idle idleState;



    //rigidbody used for any movement of enemy
    public Rigidbody2D rb;
    //target is player but can be anything really
    public GameObject target;
    //to correct the side the enemy flips too
    public bool flip;

    //for the charge and run state
    //public float chargeSpeed;
    //public float runSpeed;
    
    private void Awake() 
    {
        idleState = new Idle(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}