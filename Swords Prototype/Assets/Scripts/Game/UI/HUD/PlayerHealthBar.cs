using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this Script to the Health bar object in the HUD structure.
/// </summary>
public class PlayerHealthBar : MonoBehaviour
{
    PlayerController player;
    Slider healthSlider;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        healthSlider = GetComponentInChildren<Slider>();

        healthSlider.maxValue = player.StartHealth;
        healthSlider.value = player.StartHealth;

    }

    /// <summary>
    /// Updates the Slide value to the player's current health.
    /// </summary>
    public void UpdatePlayerHealthBar()
    {
        healthSlider.value = player.CurrentHealth;
    }
}
