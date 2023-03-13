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

    protected bool mustTurn;

    //objects body to help move it with velocity
    [SerializeField]
    public Rigidbody2D rb;

    //detection to see if object has reached the end of patrol area
    [SerializeField]
    protected Transform groundCheck;

    //layer that is labeled ground and enemy
    [SerializeField]
    protected LayerMask groundLayer;
    [SerializeField]
    protected LayerMask enemyLayer;

    //helps to turn when hitting a wall by checking if there was a collision
    [SerializeField]
    protected Collider2D bodyCollider;

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
            Patrol();
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
    public virtual void Patrol()
    {
        //checking if enemy has reached the end the road
        CheckGroundLayer();
        //moves the object
        rb.velocity = new Vector2(patrolSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    //if it turns the sprite will also turn in the given direction
    public virtual void Flip()
    {
        isIdle = true;
        transform.localScale = new Vector2(transform.localScale.x *-1, transform.localScale.y);
        if (isPatroling)
        {
            ChangeSpeed();
            isIdle = false;
        }
        
    }

    public virtual void CheckGroundLayer()
    {
        //calls Flip if the object has reached the end or hits a wall
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer) || bodyCollider.IsTouchingLayers(enemyLayer) )
        {
            if (isPatroling)
            {
                Flip();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check that they didnt collide with on the head
        if (collision.gameObject.TryGetComponent(out PlayerHealth player))
        {
            Vector3 dir = (collision.gameObject.transform.position - gameObject.transform.position).normalized;
            //checks that object enemy collided with didn't land on top of enemy
            if (dir.y < 0)
            {
                //damages player if enemy is not stunned and player has not already taken damage
                if (!player.WasJustDamaged && !IsStunned)
                {
                    player.TakeDamage(attack);
                }
            }
            
        }
    }

    public virtual void ChangeSpeed()
    {
        patrolSpeed *= -1;
    }

    public virtual void DestroyObject()
    {
        Destroy(gameObject);
    }

}
