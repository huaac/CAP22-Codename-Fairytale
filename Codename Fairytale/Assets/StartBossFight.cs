using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartBossFight : MonoBehaviour
{
    public UnityEvent onBossFightStarted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("on trigger enter");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player collided");
            onBossFightStarted?.Invoke();
        }
    }
}
