using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoelaceUI : MonoBehaviour
{
    [SerializeField] private Image shoelaceUI;

    private void Awake()
    {
        shoelaceUI.color = new Color(1f, 1f, 1f, 0f);   // transparent
    }

    public void ShowShoelaceUI()
    {
        shoelaceUI.color = new Color(1f, 1f, 1f, 1f);   // opaque
    }

    public void FadeShoelaceUI()
    {
        shoelaceUI.color = new Color(1f, 1f, 1f, 0.4f); // semi-transparent
    }
}
