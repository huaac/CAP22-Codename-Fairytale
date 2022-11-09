using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // flashing
        /*
        for (int i = 0; i < flashLength; i++)
        {
            m_sprite.color = new Color(1f, 1f, 1f, 0f); // transparent
            yield return new WaitForSeconds(0.08f);
            m_sprite.color = new Color(1f, 1f, 1f, 1f); // opaque
            yield return new WaitForSeconds(0.08f);
        }*/
        OnPlayerStartFlashing(flashLength, flashInterval);
        yield return new WaitForSeconds(flashLength * flashInterval * 2);

        wasJustDamaged = false;
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
