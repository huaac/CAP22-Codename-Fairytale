using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to check if the player stepped on an enemy by using a tiny trigger at the feet of the player.
/// </summary>

public class StepOnEnemyCheck : MonoBehaviour
{
    public delegate void OnSteppedOnEnemy();
    public static event OnSteppedOnEnemy onStepped;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            onStepped?.Invoke();
        }
    }
}
