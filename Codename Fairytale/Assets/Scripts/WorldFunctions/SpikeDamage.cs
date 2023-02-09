using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    private CheckPointSaver cpSaver;
    [SerializeField] private int attackDamage;


    // Start is called before the first frame update
    void Start()
    {
        cpSaver = GameObject.FindGameObjectWithTag("Player").GetComponent<CheckPointSaver>();
        attackDamage = 5;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.TryGetComponent(out PlayerHealth player))
        {
            cpSaver.WarpPlayerToCheckPoint();
            if (!player.WasJustDamaged)
            {
                player.TakeDamage(attackDamage);
            }
        }
    }
}
