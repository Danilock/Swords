using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemyWave 
{
    public string waveName;
    public List<EnemyController> enemiesOnWave;
    public UnityEvent OnWaveClear;
}
