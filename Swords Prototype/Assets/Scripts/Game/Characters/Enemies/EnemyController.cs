﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Audio;
using UnityEngine.Events;
public class EnemyController : MonoBehaviour
{
    #region Events
    public UnityEvent OnTakeDamage;
    public UnityEvent OnEnemyDead;
    public UnityEvent OnNormalStateUpdate;
    #endregion
    #region Enemy Stats
    public ScriptableEnemy enemyProfile;
    [HideInInspector] public float currentHealth { get; private set; }
    [HideInInspector] public float maxHealth { get; private set; }
    #endregion
    #region Gameplay
    Rigidbody2D rgb2D;
    PlayerController player;
    [HideInInspector] public Animator enemyAnimator;
    Vector2 startPosition;
    GameManager gm;
    #endregion
    #region FSM
    public EnemyNormalState normalState = new EnemyNormalState();
    public EnemyTakingDamageState takingDamageState = new EnemyTakingDamageState();
    public EnemyDeadState deadState = new EnemyDeadState();
    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyBaseState currentState { get; private set; }
    #endregion

    private void Awake()
    {
        maxHealth = enemyProfile.baseHealth;
        currentHealth = maxHealth;

        SetEnemyState(normalState);
    }

    private void Start()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();

        startPosition = transform.position;
        gm = FindObjectOfType<GameManager>();
        gm.OnLevelRestart.AddListener(RestartEnemyPosition);
    }

    private void Update()
    {
        currentState.Update(this);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            OnEnemyDead?.Invoke();
        }
        else
        {
            StopAllCoroutines();
            OnTakeDamage?.Invoke();
        }
    }

    /// <summary>
    /// Push the enemy in the direction player is facing.
    /// </summary>
    public void PushEnemy()
    {
        rgb2D?.AddForce(new Vector2(Mathf.Sign(player.transform.localScale.x) * 1f, 1.5f), ForceMode2D.Impulse);
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    public void KillWaveInstance()
    {
        EnemyWaveController waveController = FindObjectOfType<EnemyWaveController>();
        waveController.EnemyKilled(gameObject.name);
    }

    public void DropItem(Item itemToDrop)
    {
        Item instance = Instantiate(itemToDrop, transform.position, Quaternion.identity);
    }

    void RestartEnemyPosition()
    {
        transform.position = startPosition;
    }

    public void ChangeLayer(string layerName) => gameObject.layer = LayerMask.NameToLayer(layerName);

    #region Set States
    public void SetEnemyState(EnemyBaseState newState)
    {
        if (currentState != null)
            currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
    /// <summary>
    /// Set enemy state depending on int parameter.
    /// </summary>
    /// <param name="stateIndex"></param>
    public void SetEnemyState(int stateIndex)
    {
        switch (stateIndex)
        {
            case 1:
                SetEnemyState(normalState);
                break;
            case 2:
                SetEnemyState(takingDamageState);
                break;
            case 3:
                SetEnemyState(attackState);
                break;
            case 4:
                SetEnemyState(deadState);
                break;
        }
    }

    public void SetNormalState() => SetEnemyState(normalState);
    #endregion

    #region Stun
    /// <summary>
    /// Starts the stunned corroutine.
    /// </summary>
    /// <param name="time"></param>
    public void StunEnemy(float time)
    {
        StartCoroutine(Stunned(time));
        enemyAnimator.SetBool("Stunned", true);
    }

    public IEnumerator Stunned(float time)
    {
        yield return new WaitForSeconds(time);
        enemyAnimator.SetBool("Stunned", false);
        SetEnemyState(normalState);
    }
    #endregion
}
