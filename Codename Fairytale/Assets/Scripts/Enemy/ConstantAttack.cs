using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantAttack : MonoBehaviour
{
    [Header("Where?")]
    //where the circle detecting object will go
    [SerializeField]
    private Transform damagePoint;
    //what the circle range will be
    [SerializeField]
    private float damageRange;

    [Header("Who?")]
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private LayerMask targetLayers;

    [Header("How Much Damage?")]
    [SerializeField]
    private int attackDamage;

    [Header("Adjustments?")]
    [SerializeField]
    private float upDown;
    [SerializeField]
    private float rightLeft;

    private void FixedUpdate() 
    {
        if (target != null)
        {
            Attack();
        }
        
    }

    //attacks player only when they were not just attacked
    private void Attack()
    {
        if (damagePoint != null)
        {
            Vector3 attackPoint = new Vector3(damagePoint.position.x + rightLeft, damagePoint.position.y + upDown, damagePoint.position.z);
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint, damageRange, targetLayers);

            foreach (Collider2D hit in enemiesHit)
            {
                if (hit.gameObject.TryGetComponent(out PlayerHealth target))
                {
                    if (!target.WasJustDamaged)
                    {
                        target.TakeDamage(attackDamage);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(damagePoint.position, damageRange);
    }
}
