using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{

    //speed at which object will follow target (not directly used)
    public float speed = 2.0f;
    //helps reposition y axis
    public float yOffset;
    public float xOffset;

    public Vector3 minValues, maxValues;

    [HideInInspector]
    public bool _followObject;

    //target being followed
    [SerializeField]
    private Transform _targetToFollow;


    private void Awake() 
    {
        _followObject = true;
    }
    
    private void FixedUpdate () 
    {
        if(_followObject)
        {
            Follow();
        }
    }

    private void Follow()
    {
        //verify if target out of bounds
        Vector3 _position = new Vector3(_targetToFollow.position.x + xOffset, _targetToFollow.position.y + yOffset, -10f);
        Vector3 boundPosition = new Vector3
        (
            Mathf.Clamp(_position.x, minValues.x, maxValues.x),
            Mathf.Clamp(_position.y, minValues.y, maxValues.y),
            Mathf.Clamp(_position.z, minValues.z, maxValues.z)
        );

        this.transform.position = Vector3.Slerp(this.transform.position, boundPosition, speed*Time.deltaTime);
        if (this.transform.position.x >= maxValues.x)
        {
            _followObject = false;
        }
        
    }

    public void StopFollowing()
    {
        _followObject = false;
    }
}
