using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class EnemyController : MonoBehaviour
{
    #region Events
    [SerializeField] UnityEvent OnTakeDamage;
    [SerializeField] UnityEvent OnEnemyDead;
    #endregion
    #region Enemy Stats
    public ScriptableEnemy enemyProfile;
    [HideInInspector] public float currentHealth { get; private set; }
    [HideInInspector] public float maxHealth { get; private set; }
    #endregion
    #region Gameplay
    Rigidbody2D rgb2D;
    PlayerController player;
    #endregion
    #region FSM
    public EnemyNormalState normalState = new EnemyNormalState();
    public EnemyTakingDamageState takingDamageState = new EnemyTakingDamageState();
    public EnemyDeadState deadState = new EnemyDeadState();
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
        player = FindObjectOfType<PlayerController>();
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
            OnEnemyDead.Invoke();
        }
        else
        {
            SetEnemyState(takingDamageState);
            OnTakeDamage.Invoke();
        }
    }

    /// <summary>
    /// Push the enemy in the direction player is facing.
    /// </summary>
    public void PushEnemy()
    {
        rgb2D?.AddForce(new Vector2(Mathf.Sign(player.transform.localScale.x) * 1f, 1f), ForceMode2D.Impulse);
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }

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
                SetEnemyState(deadState);
                break;
        }
    }
}
