using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Infinite parallax scrolling for a background sprite.
/// Sprite must be a child object of the camera, and for infinite scrolling must be 3 units long.
/// </summary>

public class ParallaxScroll : MonoBehaviour
{
    [Tooltip("0 = no scroll, sprite stays still\n1 = scolls as fast as foreground")]
    [SerializeField][Range(0, 1)] private float parallax;

    private Transform cam;
    private Vector3 prevCam;

    private float length;

    private void Awake()
    {
        cam = Camera.main.transform;
        prevCam = cam.position;

        length = GetComponent<SpriteRenderer>().bounds.size.x / 3;
    }

    private void LateUpdate()
    {
        // parallax
        Vector3 distance = (prevCam - cam.position) * parallax;
        transform.position += distance;

        prevCam = cam.position;

        // infinite scrolling
        if (Mathf.Abs(cam.position.x - transform.position.x) >= length)
        {
            transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);
        }
    }
}
