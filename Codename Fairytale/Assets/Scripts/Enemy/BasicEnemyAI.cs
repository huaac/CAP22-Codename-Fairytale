using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour, IEnemy
{
    //patrol

    // a bool to keep track of if an enemy is stunned (bc it was stepped on)
    // is turned on/off during a Coroutine in EnemyHealth script
    //[HideInInspector] public bool isStunned;
    public bool IsStunned { get; set; }

    public float patrolSpeed;

    //can be used in other scripts when wanting to attack and stop patroling for example
    [HideInInspector]
    public bool isPatroling;

    [HideInInspector]
    public bool isIdle;

    private bool mustTurn;

    //objects body to help move it with velocity
    [SerializeField]
    public Rigidbody2D rb;

    //detection to see if object has reached the end of patrol area
    [SerializeField]
    private Transform groundCheck;

    //layer that is labeled ground and enemy
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask enemyLayer;

    //helps to turn when hitting a wall by checking if there was a collision
    [SerializeField]
    private Collider2D bodyCollider;

    [SerializeField] private int attack;
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isPatroling = true;
        isIdle = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // return out of Update() while I'm stunned so I don't move around
        if (IsStunned) return;

        if (isPatroling && !isIdle)
        {
            patrolSpeed = Patrol(patrolSpeed);
        }
    }

    //dealing with physics basically checking if object reaches the end of platform
    private void FixedUpdate()
    {
        // return out of Update() while I'm stunned so I don't move around
        if (IsStunned) return;

        if (!isIdle)
        {
            //is true if object reaches the end of platform
            mustTurn = !Physics2D.OverlapCircle(groundCheck.position, 0.7f, groundLayer);
        }
    }

    //gets the object to patrol
    public virtual float Patrol(float speed)
    {
        //checking if enemy has reached the end the road
        speed = CheckGroundLayer(speed);
        //moves the object
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
        return speed;
    }

    //if it turns the sprite will also turn in the given direction
    private float Flip(float speedF)
    {
        isIdle = true;
        transform.localScale = new Vector2(transform.localScale.x *-1, transform.localScale.y);
        speedF *= -1;
        isIdle = false;
        return speedF;
    }

    public virtual float CheckGroundLayer(float givenSpeed)
    {
        //calls Flip if the object has reached the end or hits a wall
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer) || bodyCollider.IsTouchingLayers(enemyLayer))
        {
            givenSpeed = Flip(givenSpeed);
        }
        return givenSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth player))
        {
            if (!player.WasJustDamaged && !IsStunned)
            {
                player.TakeDamage(attack);
            }
        }
    }

}
