using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Slider leftSlider;

    [SerializeField] private Slider rightSlider;

    public void UpdateLeftPlayerHealthBar(float currentHealth, float maxHealth)
    {
        if (leftSlider != null)
        { leftSlider.value = currentHealth / maxHealth; }

    }

    public void UpdateRightPlayerHealthBar(float currentHealth, float maxHealth)
    {
        if (rightSlider != null)
        { rightSlider.value = currentHealth / maxHealth; }
    }



    // Update is called once per frame
    void Update() { }
}