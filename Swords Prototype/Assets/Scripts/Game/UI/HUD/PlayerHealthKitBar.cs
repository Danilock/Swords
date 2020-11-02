using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthKitBar : MonoBehaviour
{
    private PlayerHealthKitController healthKit;
    private Slider healthKitSlider;

    private void Start()
    {
        healthKit = FindObjectOfType<PlayerHealthKitController>();
        healthKitSlider = GetComponent<Slider>();
        
        //Sets the max  value and current value of player health kit 
        healthKitSlider.maxValue = healthKit.MAXHealthKitValue;
        healthKitSlider.value = healthKit.CurrentHealthKitValue;
    }

    /// <summary>
    /// Updates the UI slide bar value.
    /// </summary>
    public void UpdateHealthKitValue()
    {
        healthKitSlider.value = healthKit.CurrentHealthKitValue;
    }
}
