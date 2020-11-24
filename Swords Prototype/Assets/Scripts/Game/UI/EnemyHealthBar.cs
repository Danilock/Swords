using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    EnemyController enemy;
    Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
        healthSlider = GetComponentInChildren<Slider>();
        enemy.OnTakeDamage.AddListener(UpdateHealthBar);
    }

    public void UpdateHealthBar()
    {
        healthSlider.value = enemy.currentHealth / enemy.maxHealth;
    }
}
