using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;
    [SerializeField] private float stunTime = 5f;

    private IEnemy enemyMovement;

    private void Awake()
    {
        enemyMovement = GetComponent<IEnemy>();
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
        enemyMovement.IsStunned = true;

        yield return new WaitForSeconds(stunTime);

        enemyMovement.IsStunned = false;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
