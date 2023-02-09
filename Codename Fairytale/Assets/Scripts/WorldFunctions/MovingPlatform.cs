using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 point1;
    [SerializeField] private Vector2 point2;

    [Header("Move to point1 when game starts", order = 0)]
    [Space(-13, order = 1)]
    [Header("(if unchecked, moves to point2 instead)", order = 2)]
    [SerializeField] private bool toPoint1;

    [SerializeField] private float speed;

    private Vector3 endPoint;
    private Vector3 startPoint;

    private Vector3 location1;
    private Vector3 location2;

    private Rigidbody2D m_rb;
    private Collider2D m_collider;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        location1 = transform.position + (Vector3)point1;
        location2 = transform.position + (Vector3)point2;

        startPoint = toPoint1 ? location2 : location1;
        endPoint = toPoint1 ? location1 : location2;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, endPoint, step);

        // when the platform reaches the "end", set the initial position as the new "end" to move to
        if (transform.position == endPoint)
        {
            Vector3 newStartPoint = endPoint;
            endPoint = startPoint;
            startPoint = newStartPoint;
        }
    }

    // make the player object a child of the moving platform,
    // subscribe DisableCollider to PlayerMovement's down button press
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            player.transform.SetParent(gameObject.transform, true);
            player.OnPlayerPressedDown += DisableCollider;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            player.transform.parent = null;
            player.OnPlayerPressedDown -= DisableCollider;
        }
    }

    // listens to event from PlayerMovement on down button press
    // disables the platform's collider for a split second so the player can fall through it
    public void DisableCollider()
    {
        StartCoroutine(DisableColliderCoroutine());
    }
    private IEnumerator DisableColliderCoroutine()
    {
        m_collider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        m_collider.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + (Vector3)point1, transform.position + (Vector3)point2);
        Gizmos.DrawSphere(transform.position + (Vector3)point1, 0.05f);
        Gizmos.DrawSphere(transform.position + (Vector3)point2, 0.05f);
    }
}
