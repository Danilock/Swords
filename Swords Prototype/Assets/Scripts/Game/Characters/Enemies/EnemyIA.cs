﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[RequireComponent(typeof(CharacterController2D))]
public class EnemyIA : MonoBehaviour
{
    #region Events
    [SerializeField] public UnityEvent OnDetectTarget;
    [SerializeField] UnityEvent OnExitTarget;
    #endregion
    #region Behaviour
    [SerializeField] Vector2 detectTargetAreaSize = new Vector3(4f, 4f);
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float reachDistance = .3f;
    EnemyController enemy;
    CharacterController2D ch2D;
    [HideInInspector] public Collider2D detectedTarget;
    float direction;
    bool isStoped, targetExit; //Target exit: used to avoid call the OnExitTarget every frame and just once.
    #endregion

    private void Start()
    {
        ch2D = GetComponent<CharacterController2D>();
        enemy = GetComponent<EnemyController>();
    }

    /// <summary>
    /// Generates a box area to found the target.
    /// </summary>
    public void GenerateDetectArea()
    {
        detectedTarget = Physics2D.OverlapBox(transform.position, detectTargetAreaSize, 0f, targetLayer);

        if (detectedTarget)
        {
            targetExit = true;
            OnDetectTarget.Invoke();
        }
        else
        {
            if (targetExit)
            {
                OnExitTarget.Invoke();
                targetExit = false;
            }
        }
    }

    /// <summary>
    /// Moves the enemy depending on where's the player at.
    /// </summary>
    public void MoveEnemy()
    {
        if (isStoped || Vector2.Distance(detectedTarget.transform.position, transform.position) < reachDistance)
            return;
        direction = Mathf.Sign(detectedTarget.transform.position.x - transform.position.x);
        ch2D.Move(direction, false, false); 
    }

    #region Detect area size
    /// <summary>
    /// Change X value of detect area size.
    /// </summary>
    /// <param name="value"></param>
    public void ChangeDetectAreaSizeInX(float value)
    {
        detectTargetAreaSize.x = value;
    }

    /// <summary>
    /// Change Y value of detect area size.
    /// </summary>
    /// <param name="value"></param>
    public void ChangeDetectAreaSizeInY(float value)
    {
        detectTargetAreaSize.y = value;
    }
    #endregion

    public void setWalkAnimation(bool state)
    {
        enemy.enemyAnimator.SetBool("Walking", state);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red * .5f;
        Gizmos.DrawWireCube(transform.position, detectTargetAreaSize);
    }
}
