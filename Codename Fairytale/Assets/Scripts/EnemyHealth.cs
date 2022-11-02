using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;
    [SerializeField] private float stunTime = 5f;
    [SerializeField] private Slider healthBar;

    [SerializeField] private int maxHealth = 100;

    private IEnemy enemyMovement;

    private void Awake()
    {
        enemyMovement = GetComponent<IEnemy>();

        healthBar.minValue = 0;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();

        Debug.Log("taken damage");
        healthBar.value = health;
    }

    public void Stun()
    {
        StartCoroutine(StunCoroutine());
        TakeDamage(20); // added from alice for testing delete later
        Debug.Log("stunned");
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
