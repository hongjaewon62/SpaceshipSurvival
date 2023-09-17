using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Slider hpSlider;

    private void Awake()
    {
        hpSlider = GetComponent<Slider>();
    }
    public void SetMaxHealth(float health)
    {
        hpSlider.maxValue = health;
        hpSlider.value = health;
    }

    public void SetHealth(float health)
    {
        hpSlider.value = health;
    }
}
