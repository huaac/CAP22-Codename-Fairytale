using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemyAI : BasicEnemyAI
{
    //pared with Facingarget, so need to find a way to make them be together at all times
    public bool isChargeing;
    public float chargeSpeed;


    public GameObject target;

    private FacingTarget _FT;


    protected override void Start()
    {
        base.Start();
        isChargeing = false;

        //this will be used to change the speed of the object to go towards target
        _FT = this.gameObject.GetComponent<FacingTarget>();
        _FT.generalSpeed = chargeSpeed;
    }

    protected override void Update()
    {
        base.Update();
        //makes sure it is charging
        if (isChargeing && !isIdle)
        {
            _FT.activatePursuit = true;
            //makes patroling false which stops object from just patroling
            if (isPatroling)
            {
                isPatroling = false;
            }
            //if target has not died
            if (target != null)
            {
                ChargeEnemy();
            }
        }
        else if (!isChargeing && !isIdle)
        {
            isPatroling = true;
        }
        if (target == null)
        {
            _FT.activatePursuit = false;
        }

    }

    public virtual void ChargeEnemy()
    {
        //generalSpeed will be updated constantly so always need to update chargeSpeed to charge in correct direction
        chargeSpeed = _FT.generalSpeed;
        //this checks if edge of platform or wall was hit and needing to change direction
        CheckGroundLayer();
        // charges at player from set charge speed
        rb.velocity = new Vector2(chargeSpeed * Time.fixedDeltaTime, rb.velocity.y);

        //fix rotation of enemy in case player bumps into boss
        Vector3 eulerRotation = this.transform.rotation.eulerAngles;
        this.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

    public override void CheckGroundLayer()
    {
        base.CheckGroundLayer();
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer) || bodyCollider.IsTouchingLayers(enemyLayer))
        {
            if (isChargeing)
            {
                Flip();
            }
        }
        Debug.Log("works2");
    }

    public override void Flip()
    {
        base.Flip();
        if (isChargeing)
        {
            ChangeSpeed();
            isIdle = false;
        }
        Debug.Log("works");
    }

    public virtual void ChangeSpeed()
    {
        base.ChangeSpeed();
        chargeSpeed *= -1;
        Debug.Log("works1");
    }


}
