using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Explotion : MonoBehaviour
{
    PlayerController player;

    [SerializeField] float explotionDamage;
    [SerializeField] Vector2 explotionSize;
    [SerializeField] GameObject explotionEffect;
    [SerializeField] Transform explotionInitArea;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    /// <summary>
    /// Make an explotion in certain area and instantiate the explotion prefab.
    /// </summary>
    public void DoExplotion()
    {
        bool detectPlayer = Physics2D.OverlapBox(transform.position, explotionSize, 0, LayerMask.GetMask("Player"));

        if (detectPlayer)
            player.TakeDamage(explotionDamage);

        Instantiate(explotionEffect, explotionInitArea.position, Quaternion.identity);
        Destroy(gameObject);
    }
    /// <summary>
    /// Starts the corroutine 'ExplotionCorroutine' and pases the 'time' as a paremeter.
    /// </summary>
    /// <param name="time"></param>
    public void ExplotionTiming(float time) => StartCoroutine(ExplotionCorroutine(time));
    IEnumerator ExplotionCorroutine(float time)
    {
        yield return new WaitForSeconds(time);
        DoExplotion();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, explotionSize);
    #if UNITY_EDITOR	
        Handles.Label(transform.position + (new Vector3(explotionSize.x, explotionSize.y, 0f) / 2), "Explotion Area");
    #endif
    }
}
