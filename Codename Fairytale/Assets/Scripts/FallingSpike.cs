using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField] private float detectDistance = 20f;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask ground;

    private Rigidbody2D m_rb;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, detectDistance, player);

        if (hit.collider != null)
        {
            m_rb.bodyType = RigidbodyType2D.Dynamic;
            m_rb.simulated = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ground == (ground | (1 << collision.gameObject.layer)))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - detectDistance, transform.position.z));
    }
}
