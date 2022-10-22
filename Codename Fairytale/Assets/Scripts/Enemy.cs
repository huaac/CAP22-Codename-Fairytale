using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a base interface describing functionality that all enemies (including bosses) should be able to do
/// </summary>

public interface IEnemy
{
    public bool IsStunned { get; set; }
}
