using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distanceAboveTarget;

    private void FixedUpdate()
    {
        transform.position = new Vector2(target.position.x, target.position.y + distanceAboveTarget);
    }
}
