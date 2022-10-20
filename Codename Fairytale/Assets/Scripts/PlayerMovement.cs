using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private BoxCollider2D m_collider;
    private PlayerState m_playerState;

    // movement variables
    [Header("Basic movement settings")]
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private float m_jumpSpeed = 7f;
    [SerializeField] private LayerMask jumpableGround;
    float movement_x = 0f;

    // attack variables
    [Header("Attack settings")]
    [SerializeField] private int attack;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    private bool isSteppingOnEnemy = false;

    // dash variables
    [Header("Dash settings")]
    [SerializeField] private float dashPower = 20f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCoolDown = 1f;
    private bool canDash = true;
    private bool isDashing = false;


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
        // disable movement input while dashing
        if (isDashing) return;

        // horizontal movement
        movement_x = Input.GetAxisRaw("Horizontal");

        // flip character
        if (movement_x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        m_rb.velocity = new Vector2(movement_x * m_moveSpeed * m_playerState.SpeedMultiplier, m_rb.velocity.y);

        // jump
        if (Input.GetAxisRaw("Jump") > 0) 
        {
            if (IsGrounded())
            {
                m_rb.velocity = new Vector2(m_rb.velocity.x, m_jumpSpeed);
            }
        }

        // dash
        // TODO: decide on dash controls
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        // slash attack
        if (Input.GetButtonDown("Slash"))
        {
            Attack();
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

    private IEnumerator Dash()
    {
        // do dash
        canDash = false;
        isDashing = true;
        float originalGravity = m_rb.gravityScale;
        m_rb.gravityScale = 0f;

        m_rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashTime);

        // stop dash
        m_rb.gravityScale = originalGravity;
        isDashing = false;

        // cooldown after dash
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    private void Attack()
    {
        // TODO: play attack animation

        // detect enemies in hitbox
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // damage them
        foreach (Collider2D hit in enemiesHit)
        {
            if (hit.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.Damage(attack);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if I collided with an enemy
        if (collision.gameObject.TryGetComponent(out EnemyHealth enemy))
        {
            // if I'm stepping on it
            if (isSteppingOnEnemy && m_rb.velocity.y < 0)
            {
                // stun enemy
                enemy.Stun();
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
