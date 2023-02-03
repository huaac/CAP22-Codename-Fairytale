using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingTarget : MonoBehaviour
{
    public GameObject target;

    public bool facingR = true;
    //to correct the side the enemy flips too
    public bool flipIt;

    [HideInInspector]
    public float generalSpeed;

    public bool isFacingTarget;
    
    public bool activatePursuit;


    // Start is called before the first frame update
    void Start()
    {
        isFacingTarget = false;  
        activatePursuit = false; 
    }

    // Update is called once per frame
    void Update()
    {
        //face player
        Vector2 scale = this.transform.localScale;
        if (target != null)
        {
            if (activatePursuit)
            {
                //if boss facing left but player on right side this fixes that and vice versa
                if (target.transform.position.x > this.transform.position.x)
                {
                    scale.x = Mathf.Abs(scale.x) * -1 * (flipIt ? -1 : 1);
                    //facing "right"
                    facingR = true;
                }
                else 
                {
                    scale.x = Mathf.Abs(scale.x) * (flipIt ? -1 : 1);
                    //facing "left"
                    facingR = false;
                }
                //fixes the chargeSpeed so that is will be charging the right way
                if (facingR)
                {
                    generalSpeed = Mathf.Abs(generalSpeed);
                }
                else
                {
                    generalSpeed = -1 * Mathf.Abs(generalSpeed);
                }
            }
            else if (!activatePursuit)
            {
                isFacingTarget = false;
            }
            isFacingTarget = true;
        }
        

        this.transform.localScale = scale;
    }
}
