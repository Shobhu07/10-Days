using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI percentageText;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;       
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
        float percentage = Mathf.Round((health / (float)slider.maxValue) * 100f);
        percentageText.text = percentage.ToString() + "%";
    }
}
