using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    [SerializeField] EnemyWave[] waves;

    private void Start()
    {
        #region InitializingEnemyWaves
        foreach (EnemyWave currentWave in waves)
        {
            foreach (EnemyController currentEnemy in currentWave.enemiesOnWave)
            {
                //Adding the killWaveInstance method to the OnEnemyDead Listener.
                currentEnemy.OnEnemyDead.AddListener(currentEnemy.KillWaveInstance);
            }
        }
        #endregion
    }

    /// <summary>
    /// Evaluates every wave list and check if the name specified in the parameter matchs one of the elements.
    /// </summary>
    /// <param name="name"></param>
    public void EnemyKilled(string name)
    {
        foreach (EnemyWave currentWave in waves) {
            for (int i = 0; i < currentWave.enemiesOnWave.Count; i++)
            {
                if (currentWave.enemiesOnWave[i].name == name) //If founds an element similar to the parameter.
                { 
                    currentWave.enemiesOnWave.Remove(currentWave.enemiesOnWave[i]); //Remove the element founded.
                    if(currentWave.enemiesOnWave.Count == 0)//if the current wave list is equal to 0, then calls de on clear event.
                    {
                        currentWave.OnWaveClear.Invoke();
                    }
                }
            }
        }
    }
}
