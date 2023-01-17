using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemyAI : BasicEnemyAI
{
    public bool isChargeing;
    public float chargeSpeed;

    public GameObject target;

    private FacingTarget _FT;


    protected override void Start()
    {
        base.Start();
        isChargeing = false;

        _FT = this.gameObject.GetComponent<FacingTarget>();
        _FT.generalSpeed = chargeSpeed;
    }

    protected override void Update()
    {
        base.Update();
        if (isChargeing && !isIdle)
        {
            _FT.activatePursuit = true;
            if (isPatroling)
            {
                isPatroling = false;
            }
            if (target != null)
            {
                ChargeEnemy();
            }
        }
        if (target == null)
        {
            _FT.activatePursuit = false;
        }

    }

    public virtual void ChargeEnemy()
    {
        chargeSpeed = _FT.generalSpeed;
        chargeSpeed = CheckGroundLayer(chargeSpeed);
        // charges at player from set charge speed
        rb.velocity = new Vector2(chargeSpeed * Time.fixedDeltaTime, rb.velocity.y);

        //fix rotation of enemy in case player bumps into boss
        Vector3 eulerRotation = this.transform.rotation.eulerAngles;
        this.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }


}
