using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    #region FSM(State Machine)
    public PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerMovingState movingState = new PlayerMovingState();
    public PlayerJumpState jumpState = new PlayerJumpState();
    public PlayerAttackState attackState = new PlayerAttackState();
    public PlayerDeadState deadState = new PlayerDeadState();
    public PlayerDamagedState damagedState = new PlayerDamagedState();
    public PlayerBowAttackState bowAttackState = new PlayerBowAttackState();
    #endregion
    #region Gameplay
    [SerializeField] float startHealth = 40f;
    public float StartHealth { get { return startHealth; } private set { startHealth = value; } }
    public float CurrentHealth { get; private set; }
    [SerializeField, Range(1, 2)] float speed;
    [HideInInspector] public CharacterController2D ch2D;
    [HideInInspector] public float horizontalMove;
    [HideInInspector] public Rigidbody2D rgb2D;
    [HideInInspector] public PlayerAttackController attackController;
    #endregion
    #region Events
    [SerializeField] UnityEvent OnPlayerDead;
    [SerializeField] UnityEvent OnPlayerTakeDamage;
    #endregion
    #region Animation
    [HideInInspector] public Animator playerAnimator;
    #endregion
    private void Awake()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        ch2D = GetComponent<CharacterController2D>();
        attackController = GetComponent<PlayerAttackController>();
        playerAnimator = GetComponent<Animator>();
        CurrentHealth = StartHealth;

        SetState(idleState);
    }

    private void Update()
    {
        if (GameManager.currentGameState == GameManager.GameState.InGame) {
            currentState.Update(this);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.currentGameState == GameManager.GameState.InGame)
        {
            currentState.FixedUpdate(this);
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0f)
        {
            OnPlayerDead?.Invoke();
            SetState(deadState);
        }
        else
        {
            OnPlayerTakeDamage?.Invoke();
            SetState(damagedState);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.currentGameState == GameManager.GameState.InGame) { 
            currentState.OnCollisionEnter2D(this, collision);
        }
    }

    /// <summary>
    /// Set a new state in the player FSM. Before stablish the new state, it executes the ExitState() from the last State.
    /// </summary>
    /// <param name="newState"></param>
    public void SetState(PlayerBaseState newState)
    {
        if(currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        newState.EnterState(this);
    }

    /// <summary>
    /// Stablish the HorizontalMove variable the input value. Also, determines if the jump key is pressed
    /// </summary>
    public void PlayerInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump"))
        {
           ch2D.Move(horizontalMove, false, true);
            SetState(jumpState);
        }
    }

    /// <summary>
    /// Set the player state to Idle inmediatly. (Use this in animations)
    /// </summary>
    public void SetIdleState()
    {
        SetState(idleState);
    }

    public bool CollidedWall()
    {
        bool linecastDetectWall = Physics2D.Linecast(transform.position,
                                               transform.position + (transform.right * transform.localScale.x * .25f),
                                               LayerMask.GetMask("Wall"));

        return linecastDetectWall;
    }

    private void OnDrawGizmos()
    {
        //Drawing gizmos for raycastLine to detect walls(this is used to stop player air movement when detect the wall)
        Gizmos.color = Color.blue * .5f;
        Gizmos.DrawLine(transform.position, transform.position + (transform.right * transform.localScale.x * .25f));
    }
}
