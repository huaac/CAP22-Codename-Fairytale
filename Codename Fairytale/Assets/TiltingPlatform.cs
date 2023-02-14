using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltingPlatform : MonoBehaviour
{
    // Layer(s) of whatever should be able to ride on this platform and tilt it.
    // Will most likely just be the player
    [SerializeField] private LayerMask cargoLayer;

    [SerializeField] private float maxRotation = 20f;

    private bool isOnLeft, isOnRight;


    private void FixedUpdate()
    {
        // tilt to which ever side of platform has something on it
        if (isOnLeft)
        {
            Vector3 newAngle = transform.eulerAngles + new Vector3(0, 0, 0.07f);
            newAngle.z = Mathf.Min(maxRotation, newAngle.z - 360f);
            transform.eulerAngles = newAngle;
        }
        else if (isOnRight)
        {
            Vector3 newAngle = transform.eulerAngles + new Vector3(0, 0, -0.07f);
            newAngle.z = Mathf.Max(-maxRotation, newAngle.z);
            transform.eulerAngles = newAngle;
        }

        else if (!isOnLeft && !isOnRight && transform.eulerAngles.z != 0f)
        {
            // adjust back to 0 rotation
            if (Mathf.Abs(transform.eulerAngles.z - 360f) <= 0.05f)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            // if rotated to right, rotate back left
            else if (transform.eulerAngles.z > 180)
            {
                transform.eulerAngles += new Vector3(0, 0, 0.07f);
            }
            // if rotated to left, rotate back right
            else if (transform.eulerAngles.z < 180)
            {
                transform.eulerAngles += new Vector3(0, 0, -0.07f);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (cargoLayer==(cargoLayer | 1 << collision.gameObject.layer))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                isOnRight = false;
                isOnLeft = true;
            }
            else
            {
                isOnLeft = false;
                isOnRight = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (cargoLayer == (cargoLayer | 1 << collision.gameObject.layer))
        {
            isOnLeft = false;
            isOnRight = false;
        }
    }
}
