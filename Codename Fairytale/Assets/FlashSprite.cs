using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnEnable()
    {
        player.OnPlayerStartFlashing += StartFlash;
    }
    private void OnDisable()
    {
        player.OnPlayerStartFlashing -= StartFlash;
    }

    public void StartFlash(float flashLen, float flashInter)
    {
        flashLength = flashLen;
        flashInterval = flashInter;
        StartCoroutine(FlashCoroutine());
    }
    private IEnumerator FlashCoroutine()
    {
        for (int i = 0; i < flashLength; i++)
        {
            sprite.color = new Color(1f, 1f, 1f, 0f); // transparent
            yield return new WaitForSeconds(flashInterval);
            sprite.color = new Color(1f, 1f, 1f, 1f); // opaque
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
