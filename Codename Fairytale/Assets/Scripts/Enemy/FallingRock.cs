using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] private int attack = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth player))
        {
            if (!player.WasJustDamaged)
            {
                player.TakeDamage(attack);
            }
        }
        Destroy(gameObject, 2.0f);
    }

    
}
