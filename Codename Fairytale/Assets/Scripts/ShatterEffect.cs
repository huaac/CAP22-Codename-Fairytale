using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterEffect : MonoBehaviour
{
    public GameObject broken_pieces;


    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void shattered()
    {
        GameObject replaced_wall = Instantiate(broken_pieces, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(replaced_wall, 3f);
    }
}
