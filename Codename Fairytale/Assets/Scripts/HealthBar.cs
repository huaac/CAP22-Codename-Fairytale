using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    //Sets the max health possible in the slider
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    //sets the updated health in the slider
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
