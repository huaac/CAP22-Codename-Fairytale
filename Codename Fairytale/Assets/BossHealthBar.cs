using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : HealthBar
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowHealthBar()
    {
        this.gameObject.SetActive(true);
    }
}
