using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Item", order = 3, menuName = "Item Profile")]
public class ScriptableItem : ScriptableObject
{
    public string itemName;
    /// <summary>
    /// If itemDurability type is set to Temporaly then itemDurability field value needs to be stablished.
    /// </summary>
    public enum itemDurabilityType
    {
        Temporaly,
        Permanent
    }

    public itemDurabilityType durabilityType;
    
    public float itemDurability;
    [FormerlySerializedAs("lifeToAdd")] public int healthKitToAdd;
    public int damageToAdd;
}
