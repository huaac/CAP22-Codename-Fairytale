using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartBossFight : MonoBehaviour
{
    private bool hasStarted = false;
    [SerializeField] private GameObject barriers;
    public UnityEvent onBossFightStarted;
    private GameObject musicSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onBossFightStarted?.Invoke();
            musicSource = GameObject.FindWithTag("Music");
            musicSource.GetComponent<constantmusic>().battleTime();
            hasStarted = true;
            barriers.SetActive(true);
            DestroySelf();
        }
    }

    private void UIstuff()
    {
    }

    private void Dialogue()
    {
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
