using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private BoxCollider2D m_collider;
    private PlayerState m_playerState;

    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private float m_jumpSpeed = 7f;
    [SerializeField] private LayerMask jumpableGround;

    private bool isSteppingOnEnemy = false;

    float movement_x = 0f;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<BoxCollider2D>();
        m_playerState = GetComponent<PlayerState>();
    }

    private void OnEnable()
    {
        StepOnEnemyCheck.onStepped += StepOnEnemy;
    }

    void Update() 
    {
        //horizontal movement
        movement_x = Input.GetAxisRaw("Horizontal");
        m_rb.velocity = new Vector2(movement_x * m_moveSpeed * m_playerState.SpeedMultiplier, m_rb.velocity.y);

        //jump
        if (Input.GetAxisRaw("Jump") > 0) 
        {
            if (IsGrounded())
            {
                m_rb.velocity = new Vector2(m_rb.velocity.x, m_jumpSpeed);
            }
        }
    }

    private void OnDisable()
    {
        StepOnEnemyCheck.onStepped -= StepOnEnemy;
    }


    private void StepOnEnemy()
    {
        isSteppingOnEnemy = true;
    }

    // A function that returns true if the player is on the ground
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(m_collider.bounds.center, m_collider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if I collided with an enemy
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            // if I'm stepping on it
            if (isSteppingOnEnemy)
            {
                // kill enemy
                enemy.Die();
                isSteppingOnEnemy = false;
            }
            else
            {
                // else, I die
                Die();
            }
        }
    }
}
