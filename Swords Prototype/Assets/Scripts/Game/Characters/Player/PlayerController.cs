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
    #endregion
    #region Gameplay
    [SerializeField] float startLife = 40f;
    [HideInInspector] public CharacterController2D ch2D;
    [HideInInspector] public float horizontalMove;
    [HideInInspector] public Rigidbody2D rgb2D;
    #endregion
    #region Events
    [SerializeField] UnityEvent OnPlayerDead;
    [SerializeField] UnityEvent OnPlayerTakeDamage;
    #endregion
    private void Start()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        ch2D = GetComponent<CharacterController2D>();

        SetState(idleState);
    }

    private void Update()
    {
        currentState.Update(this);
    }

    public void TakeDamage(float damage)
    {
        startLife -= damage;

        if(startLife <= 0f)
        {
            OnPlayerDead.Invoke();
        }
        else
        {
            OnPlayerTakeDamage.Invoke();
        }
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
}
