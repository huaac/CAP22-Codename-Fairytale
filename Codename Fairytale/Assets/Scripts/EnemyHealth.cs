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
    private int currentHealth;

    private IEnemy enemyMovement;

    private void Awake()
    {
        enemyMovement = GetComponent<IEnemy>();

        currentHealth = maxHealth;

        // safety check in case we want enemy w/o health bar
        if (healthBar)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();

        if (healthBar) healthBar.value = currentHealth;
    }

    public void Stun()
    {
        StartCoroutine(StunCoroutine());
        //TakeDamage(20); // added from alice for testing delete later
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
