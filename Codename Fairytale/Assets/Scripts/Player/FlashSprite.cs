using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to flash the player's sprite(s) when they are damaged.
/// </summary>

public class FlashSprite : MonoBehaviour
{
    private PlayerHealth player;
    private SpriteRenderer sprite;
    private float flashLength;
    private float flashInterval;

    private void Awake()
    {
        if (transform.parent.TryGetComponent(out PlayerHealth playerHealth))
        {
            player = playerHealth;
        }
        sprite = GetComponent<SpriteRenderer>();
    }

    // subscribe to PlayerHealth's on player damage
    private void OnEnable()
    {
        player.OnPlayerStartFlashing += StartFlash;
    }
    private void OnDisable()
    {
        player.OnPlayerStartFlashing -= StartFlash;
    }

    // listens to event from PlayerHealth on player damage
    public void StartFlash(float flashLen, float flashInter)
    {
        flashLength = flashLen;
        flashInterval = flashInter;
        StartCoroutine(FlashCoroutine());
    }
    private IEnumerator FlashCoroutine()
    {
        // switches between transparent & opaque for given flash length,
        // flashInterval is the tiny amount of time between the switches
        for (int i = 0; i < flashLength; i++)
        {
            sprite.color = new Color(1f, 1f, 1f, 0f); // transparent
            yield return new WaitForSeconds(flashInterval);
            sprite.color = new Color(1f, 1f, 1f, 1f); // opaque
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
