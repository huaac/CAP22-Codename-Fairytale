using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int curentHealth;
    [SerializeField] private int flashLength = 10;
    [SerializeField] private float flashInterval = 0.08f;

    [SerializeField] private HealthBar healthBar;

    private bool wasJustDamaged = false;
    public bool WasJustDamaged { get { return wasJustDamaged; } }

    // delegates
    public Action<float, float> OnPlayerStartFlashing;
    public UnityEvent OnPlayerDied;

    //set the max health for the slider, sets current health to max health
    void Start()
    {
        curentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        curentHealth -= damage;
        if (curentHealth <= 0) Die();

        healthBar.SetHealth(curentHealth);

        StartCoroutine(JustDamaged());
    }
    private IEnumerator JustDamaged()
    {
        wasJustDamaged = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);

        // dispatch player flashing event
        // all of player's limb sprites will be listening to this
        OnPlayerStartFlashing?.Invoke(flashLength, flashInterval);
        yield return new WaitForSeconds(flashLength * flashInterval * 2);

        Physics2D.IgnoreLayerCollision(6, 7, false);
        wasJustDamaged = false;
    }

    private void Die()
    {
        OnPlayerDied?.Invoke();
        Destroy(this.gameObject);
    }
}
