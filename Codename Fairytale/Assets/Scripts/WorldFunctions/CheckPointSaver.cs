using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSaver : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsCheckPoint;

    public Vector2 CheckPointLocation {get; private set;} = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        //initialize a starting safe location
        CheckPointLocation = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // if collided with checkpoint
        if ((whatIsCheckPoint.value & (1 << other.gameObject.layer)) > 0)
        {
            //update CheckPointLocation
            CheckPointLocation = new Vector2(other.bounds.center.x, other.bounds.min.y);
        }
    }

    public void WarpPlayerToCheckPoint()
    {
        transform.position = CheckPointLocation;
    }
}
