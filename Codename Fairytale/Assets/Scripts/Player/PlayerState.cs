using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a class that keeps track of the player's state.
/// Other classes can use this class by checking if an object has a PlayerState component.
/// </summary>

public class PlayerState : MonoBehaviour
{
    [SerializeField] private int playerID;

    private bool isDead;
    private bool hasFinished;

    private float speedMultiplier = 1f;


    public int PlayerID
    {
        get { return playerID; }
    }

    public bool IsDead
    {
        get { return isDead; }
    }
    public void SetToDead()
    {
        isDead = true;
    }


    public bool HasFinished
    {
        get { return hasFinished; }
    }
    public void SetToFinished()
    {
        hasFinished = true;
    }


    public float SpeedMultiplier
    {
        get { return speedMultiplier; }
    }
    public void EnableSpeed(float speed)
    {
        speedMultiplier = speed;
    }
    public void DisableSpeed()
    {
        speedMultiplier = 1f;
    }
}
