using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private BoxCollider2D m_collider;
    private SpriteRenderer m_sprite;
    private PlayerState m_playerState;

    private Animator m_anim;

    // bool's for testing purposes
    [Tooltip("Turn on/off controls for testing purposes")]
    [Header("Test controls")]
    [SerializeField] private bool dashEnabled = true;
    [SerializeField] private bool bounceOffEnemyEnabled = true;

    // movement variables
    [Header("Basic player settings")]
    /*
    [SerializeField] private int health = 100;
    [SerializeField] private int flashLength = 10;
    [SerializeField] private float flashInterval = 0.08f;*/
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

    // dash variables
    [Header("Dash settings")]
    [SerializeField] private float dashPower = 20f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCoolDown = 1f;
    private bool canDash = true;
    private bool isDashing = false;
    private bool isBouncingOff = false;

    // particle systems/vfx
    [Header("VFX")]
    [SerializeField] private ParticleSystem dustPS;
    [SerializeField] private ParticleSystem dustLandPS;
    [SerializeField] private TrailRenderer bookTR;

    // player state bool's
    private bool isSteppingOnEnemy = false;
    public bool IsStepping { get { return isSteppingOnEnemy; } }
    private bool isAttacking = false;
    private bool isOnMovingPlatform = false;
    private Rigidbody2D platformRB;

    // delegates
    public Action OnPlayerPressedDown;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<BoxCollider2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_playerState = GetComponent<PlayerState>();
        m_anim = GetComponent<Animator>();

        m_anim.SetInteger("currentState", 0);
    }

    private void OnEnable()
    {
        StepOnEnemyCheck.onStepped += StepOnEnemy;
    }

    void Update() 
    {
        // disable movement input while: dashing, just bounced off enemy
        //if (isDashing) return;
        if (isBouncingOff) return;

        // horizontal movement
        movement_x = Input.GetAxisRaw("Horizontal");

        // flip character
        Flip();

        m_rb.velocity = new Vector2(movement_x * m_moveSpeed * m_playerState.SpeedMultiplier, m_rb.velocity.y);

        // jump
        if (Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
        {
            if (IsGrounded())
            {
                m_rb.velocity = new Vector2(m_rb.velocity.x, m_jumpSpeed);
                // m_anim.SetInteger("currentState", 2);
            }
        }

        // player pressed down
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            OnPlayerPressedDown();
        }

        // dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashEnabled && canDash)
        {
            StartCoroutine(Dash());
        }

        // slash attack
        if (Input.GetButtonDown("Slash") && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        else
        {
            DoAnimations();
        }
    }

    // flip player sprite according to movement direction
    private void Flip()
    {
        if (movement_x < 0)
        {
            transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
        }
        else if (movement_x > 0)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
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
    private void BounceOffEnemy()
    {
        StartCoroutine(BounceOffCoroutine());
    }
    private IEnumerator BounceOffCoroutine()
    {
        isBouncingOff = true;
        // add diagonal force in the same direction I was moving in when I stepped enemy
        Debug.Log(m_rb.velocity.x);
        m_rb.AddForce(new Vector2(m_rb.velocity.x * 1.2f, 10f), ForceMode2D.Impulse);

        // disable movement for a split second just after bouncing off
        yield return new WaitForSeconds(0.1f);

        isBouncingOff = false;
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

    private IEnumerator Attack()
    {
        isAttacking = true;
        m_anim.SetInteger("currentState", 3);
        bookTR.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        // detect enemies in hitbox
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // damage them
        foreach (Collider2D hit in enemiesHit)
        {
            if (hit.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(attack);
            }
        }

        yield return null;

        m_anim.SetInteger("currentState", 0);

        yield return new WaitForSeconds(0.4f);

        bookTR.gameObject.SetActive(false);
        isAttacking = false;
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
                // bounce off of it
                BounceOffEnemy();

                isSteppingOnEnemy = false;
            }
        }
        // else if I landed on ground, play dust PS
        else if (jumpableGround==(jumpableGround | 1 << collision.gameObject.layer) && !dustLandPS.isPlaying)
        {
            dustLandPS.Play();
        }
    }

    void DoAnimations()
    {
        if (isAttacking) return;

        if (m_rb.velocity != Vector2.zero)
        {
            m_anim.SetInteger("currentState", 1); // change to running anim
            if (m_rb.velocity.y != 0 && dustPS.isPlaying)
            {
                dustPS.Stop();
            }
            else if (m_rb.velocity.y == 0 && !dustPS.isPlaying)
            {
                dustPS.Play();
            }
        }
        else{
            m_anim.SetInteger("currentState", 0); // change to idle anim
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
