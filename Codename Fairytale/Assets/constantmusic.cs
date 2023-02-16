using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constantmusic : MonoBehaviour
{

    private AudioSource currentSong;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        currentSong = this.GetComponent<AudioSource>();
    }


}
