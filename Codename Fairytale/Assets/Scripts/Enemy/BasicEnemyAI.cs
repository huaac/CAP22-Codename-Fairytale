using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    //patrol and charge

    public float patrolSpeed;

    //can be used in other scripts when wanting to attack and stop patroling for example
    [HideInInspector]
    public bool isPatroling;

    private bool mustTurn;

    //objects body to help move it with velocity
    [SerializeField]
    private Rigidbody2D rb;

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
    

    // Start is called before the first frame update
    void Start()
    {
        isPatroling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatroling)
        {
            Patrol();
        }
    }

    //dealing with physics basically checking if object reaches the end of platform
    private void FixedUpdate()
    {
        if (isPatroling)
        {
            //is true if object reaches the end of platform
            mustTurn = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }
    }

    //gets the object to patrol
    void Patrol()
    {
        //calls Flip if the object has reached the end or hits a wall
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer) || bodyCollider.IsTouchingLayers(enemyLayer))
        {
            Flip();
        }
        //moves the object
        rb.velocity = new Vector2(patrolSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    //if it turns the sprite will also turn in the given direction
    void Flip()
    {
        isPatroling = false;
        transform.localScale = new Vector2(transform.localScale.x *-1, transform.localScale.y);
        patrolSpeed *= -1;
        isPatroling = true;
    }
}
