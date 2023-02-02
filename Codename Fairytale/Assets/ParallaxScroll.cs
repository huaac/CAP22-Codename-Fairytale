using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    [SerializeField] private float parallax;
    private Transform cam;
    private SpriteRenderer sprite;

    private float startL, startR;
    private float length;

    private Vector3 oldCam;
    private float camWidthHalf;

    private bool madeNewRight, madeNewLeft;

    private void Awake()
    {
        cam = Camera.main.transform;
        camWidthHalf = Camera.main.orthographicSize * Camera.main.aspect / 2;
        oldCam = cam.position;

        sprite = GetComponent<SpriteRenderer>();
        length = sprite.bounds.size.x;
        startL = transform.position.x - (length / 2);
        startR = transform.position.x + (length / 2);
    }

    private void Update()
    {
        float distance = (oldCam.x - cam.position.x) * parallax;
        Vector3 newPos = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        transform.position = newPos;

        oldCam = cam.position;

        if (!madeNewRight && startL < cam.position.x - camWidthHalf)
        {
            Instantiate(gameObject, new Vector3(transform.position.x + length, transform.position.y, transform.position.z), transform.rotation);
            madeNewRight = true;
        }
        if (startR < cam.position.x - camWidthHalf)
        {
            Destroy(this.gameObject);
        }

        /*
        if (!madeNewLeft && startL > cam.position.x - camWidthHalf)
        {
            Instantiate(gameObject, new Vector3(transform.position.x - length, transform.position.y, transform.position.z), transform.rotation);
            madeNewLeft = true;
        }
        if (startR > cam.position.x + camWidthHalf)
        {
            Destroy(this.gameObject);
        }*/
    }
}
