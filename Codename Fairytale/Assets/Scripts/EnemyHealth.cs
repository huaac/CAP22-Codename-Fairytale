using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;
    [SerializeField] private float stunTime = 5f;

    private BasicEnemyAI enemyMovement;

    private void Awake()
    {
        enemyMovement = GetComponent<BasicEnemyAI>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    public void Stun()
    {
        StartCoroutine(StunCoroutine());
    }

    private IEnumerator StunCoroutine()
    {
        enemyMovement.isStunned = true;

        yield return new WaitForSeconds(stunTime);

        enemyMovement.isStunned = false;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
