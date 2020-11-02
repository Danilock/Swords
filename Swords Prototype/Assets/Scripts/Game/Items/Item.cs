using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(InteractableObject))]
public class Item : MonoBehaviour
{
    [SerializeField] private ScriptableItem itemProfile;
    [SerializeField] private UnityEvent OnItemTaked;
    private PlayerHealthBar healthBarUI;
    private PlayerController player;
    private PlayerHealthKitController healthKit;
    private PlayerHealthKitBar healthKitBar;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        healthKit = FindObjectOfType<PlayerHealthKitController>();
        healthBarUI = FindObjectOfType<PlayerHealthBar>();
        healthKitBar = FindObjectOfType<PlayerHealthKitBar>();
    }
    /// <summary>
    /// Takes the item and updates all player stats, also updates the healthbar.
    /// </summary>
    public void TakeItem()
    {
        healthKit.SetHealthKitValue(itemProfile.healthKitToAdd);
        player.attackController.ModifyBaseDamage(itemProfile.damageToAdd);
        if (healthBarUI)
        {
            healthBarUI.UpdatePlayerHealthBar();
        }

        if (healthKitBar)
        {
            healthKitBar.UpdateHealthKitValue();
        }
        
        OnItemTaked.Invoke();
    }
}
