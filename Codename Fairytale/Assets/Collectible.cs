using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    public UnityEvent OnPaperCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // do something
            OnPaperCollected?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
