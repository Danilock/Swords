using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthKitController : MonoBehaviour
{
    #region MyRegion HealthKit Values

    

    [SerializeField] private float startHealthKitValue = 0;

    public float StartHealthKitValue
    {
        get => startHealthKitValue;
        private set => startHealthKitValue = value;
    }

    [SerializeField] private float maxHealthKitValue = 100;

    public float MAXHealthKitValue
    {
        get => maxHealthKitValue;
        private set => maxHealthKitValue = value;
    }

    public float CurrentHealthKitValue { get; set; }
    #endregion

    [SerializeField] private float regenerationRate = 3f;
    private PlayerController player;
    private PlayerHealthKitBar healthKitBar;
    private bool regeneratingPlayerLife;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        healthKitBar = FindObjectOfType<PlayerHealthKitBar>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Health Kit") && CurrentHealthKitValue > 0.1f)
        {
            regeneratingPlayerLife = true;
        }

        //If regeneration life is true then updates the current health kit value and UI
        if (regeneratingPlayerLife)
        {
            player.RegenerateLife(regenerationRate);
            CurrentHealthKitValue -= regenerationRate * Time.deltaTime;
            healthKitBar.UpdateHealthKitValue();

            if (player.CurrentHealth > player.StartHealth)
            {
                player.SetLife(player.StartHealth);
                regeneratingPlayerLife = false;
            }
            else if (CurrentHealthKitValue < 0.1f)
            {
                regeneratingPlayerLife = false;
            }
        }
    }
    
    /// <summary>
    /// Additive value to Current Health kit.
    /// </summary>
    /// <param name="newHealthKitValue"></param>
    public void SetHealthKitValue(float newHealthKitValue)
    {
        CurrentHealthKitValue += newHealthKitValue;

        if (CurrentHealthKitValue > maxHealthKitValue)
        {
            CurrentHealthKitValue = maxHealthKitValue;
        }
    }
}
