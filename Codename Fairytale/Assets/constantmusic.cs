using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constantmusic : MonoBehaviour
{

    [SerializeField] private AudioClip menuSong;
    [SerializeField] private AudioClip bgSong;
    [SerializeField] private AudioClip battleSong;
    [SerializeField] private AudioClip gameOverSong;

    private AudioSource currentSource;
    // Start is called before the first frame update

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        currentSource = this.GetComponent<AudioSource>();
        currentSource.clip = menuSong;
    }

    public void gameLoad()
    {
        currentSource.clip = bgSong;
        currentSource.Play();
    }

    public void battleTime()
    {
        currentSource.clip = battleSong;
        currentSource.Play();
    }

    public void PlayGameOverMusic()
    {
        currentSource.clip = gameOverSong;
        currentSource.Play();
    }

    public void DestroyMusic()
    {
        Destroy(this.gameObject);
    }
}
