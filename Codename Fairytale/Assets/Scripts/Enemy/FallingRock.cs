using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] private int attack = 10;
    private bool hasLanded = false;
    private Collider2D m_collider;

    private void Awake()
    {
        m_collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth player))
        {
            if (!player.WasJustDamaged && !hasLanded)
            {
                player.TakeDamage(attack);
            }
        }
        hasLanded = true;
        m_collider.enabled = false;
        Destroy(gameObject, 2.0f);
    }
}
