using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartBossFight : MonoBehaviour
{
    private bool hasStarted = false;
    [SerializeField] private GameObject barriers;
    public UnityEvent onBossFightStarted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onBossFightStarted?.Invoke();
            hasStarted = true;
            barriers.SetActive(true);
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}