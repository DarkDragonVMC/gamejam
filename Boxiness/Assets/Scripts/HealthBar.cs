using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider Life;

    public void SetHealth(int health)
    {
        Life.value = health;
    }

    public void SetMaxHealth(int maxhealth)
    {
        Life.maxValue = maxhealth;
    }

}
