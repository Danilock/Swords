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
    #endregion
    #region Gameplay
    [SerializeField] float startHealth = 40f;
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
    private void Start()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        ch2D = GetComponent<CharacterController2D>();
        attackController = GetComponent<PlayerAttackController>();

        SetState(idleState);
    }

    private void Update()
    {
        currentState.Update(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }

    public void TakeDamage(float damage)
    {
        startHealth -= damage;

        if(startHealth <= 0f)
        {
            OnPlayerDead.Invoke();
        }
        else
        {
            OnPlayerTakeDamage.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this, collision);
    }

    /// <summary>
    /// Set a new state in the player FSM. Before stablish the new state, it executes the ExitState() from the last State.
    /// </summary>
    /// <param name="newState"></param>
    public void SetState(PlayerBaseState newState)
    {
        if(newState != null)
        {
            newState.ExitState(this);
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
}
