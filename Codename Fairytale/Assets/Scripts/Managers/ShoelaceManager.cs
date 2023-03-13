using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShoelaceManager : MonoBehaviour
{
    [SerializeField] private Shoelace[] laces;
    [SerializeField] private UnityEvent onLacesCompleted;

    private int slCount = 0;

    private void OnEnable()
    {
        foreach (Shoelace lace in laces)
        {
            lace.OnCollected += IncrementCount;
        }
    }

    private void OnDisable()
    {
        foreach (Shoelace lace in laces)
        {
            lace.OnCollected -= IncrementCount;
        }
    }

    private void IncrementCount()
    {
        slCount++;
        if (slCount >= 2)
        {
            onLacesCompleted?.Invoke();
        }
    }
}
