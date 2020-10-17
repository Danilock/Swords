using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable/Enemy", order = 0)]
public class ScriptableEnemy : ScriptableObject
{
    public string name;
    public float baseHealth;
    public float baseDamage;
    public float attackCooldown = 2f;
}
