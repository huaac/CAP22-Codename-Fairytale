using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartBossFight : MonoBehaviour
{
    private bool hasStarted = false;
    public UnityEvent onBossFightStarted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onBossFightStarted?.Invoke();
            hasStarted = true;
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
