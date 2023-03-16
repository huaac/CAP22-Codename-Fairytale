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
    public GameObject GoatBoss;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DialogueDelay());
        }
    }

    IEnumerator DialogueDelay()
    {
        yield return new WaitForSecondsRealtime(28.5f);
        UIstuff();
    }



    public void UIstuff()
    {
        if (!hasStarted)
        {
            GoatBoss.SetActive(true);
            onBossFightStarted?.Invoke();
            musicSource = GameObject.FindWithTag("Music");
            musicSource.GetComponent<constantmusic>().battleTime();
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
