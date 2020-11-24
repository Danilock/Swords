using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this Script to the Health bar object in the HUD structure.
/// </summary>
public class PlayerHealthBar : MonoBehaviour
{
    PlayerController player;
    Slider healthSlider;
    [SerializeField] Image fill;
    [SerializeField] Gradient fillGradient;
    [SerializeField] private TextMeshProUGUI lifeText;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        healthSlider = GetComponent<Slider>();

        healthSlider.maxValue = player.StartHealth;
        healthSlider.value = player.StartHealth;
        lifeText.text = $"{player.CurrentHealth}/{player.StartHealth}";
        
        player.OnPlayerTakeDamage.AddListener(UpdatePlayerHealthBar);
        player.OnLifeRegenerating += UpdatePlayerHealthBar;

        fill.color = fillGradient.Evaluate(1f);
    }

    private void OnDisable()
    {
        player.OnPlayerTakeDamage.RemoveListener(UpdatePlayerHealthBar);
        player.OnLifeRegenerating -= UpdatePlayerHealthBar;
    }

    /// <summary>
    /// Updates the Slide value to the player's current health.
    /// </summary>
    public void UpdatePlayerHealthBar()
    {
        healthSlider.value = player.CurrentHealth;
        lifeText.text = $"{player.CurrentHealth}/{player.StartHealth}";
        
        fill.color = fillGradient.Evaluate(healthSlider.normalizedValue);
    }
}
