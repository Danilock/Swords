using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider2D))]
public class DraggableObject : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private bool canInteract = true;
    public bool CanInteract
    {
        get => canInteract;
        set => canInteract = value;
    }

    [SerializeField] private bool returnOnDragCancelled;
    [SerializeField] private bool canCompleteDrag;

    #region TimeScaleChange
    [SerializeField, Space, Header("Check if can change game's scale")] private bool changeTimeScale;
    [SerializeField] private float timeScaleValue;
    #endregion

    #region Physics
    [FormerlySerializedAs("onCompleteArea")] [SerializeField] private Collider2D completeTargetArea;
    private Collider2D dragableObjectCollider;
    #endregion

    #region Lerping and mousePosition
    private Vector3 mousePosition, startPosition, lastPosition;
    private float lerpPct;
    private bool canReturn;
    #endregion

    #region State Machine
    private enum dragableObjectState {DragStarted, DragCompleted, DragCancelled, Idle}

    private dragableObjectState currentState;
    #endregion

    #region Events

    [SerializeField] private UnityEvent OnDragStarted, OnDragCompleted, OnDragCancelled;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        dragableObjectCollider = GetComponent<Collider2D>();
        currentState = dragableObjectState.Idle;
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == dragableObjectState.DragCompleted)
            return;
        
        if(returnOnDragCancelled && canReturn)
        {
            canInteract = false;
            lerpPct += 0.2f * Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(transform.position, startPosition, lerpPct);
            
            //if reached start position then reset lerping and canInteract
            if (Vector2.Distance(transform.position, startPosition) < .1f)
            {
                canReturn = false;
                canInteract = true;
                lerpPct = 0f;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!canInteract || currentState == dragableObjectState.DragCompleted)
            return;
        currentState = dragableObjectState.DragStarted;
        canReturn = false;
        OnDragStarted.Invoke();
        if (changeTimeScale)
        {
            GameManager.SetTimeScale(timeScaleValue);
        }
    }

    /// <summary>
    /// When somebody left the mouse the object will return to its start position.
    /// </summary>
    private void OnMouseUp()
    {
        //If detects the Target Complete Area then set's the object state to completed. 
        if (Physics2D.IsTouching(dragableObjectCollider, completeTargetArea))
        {
            OnDragCompleted.Invoke();
            currentState = dragableObjectState.DragCompleted;
            return;
        }
        
        //Saving last position to be used in the lerp operation to return to it's start position
        lastPosition = transform.position;
        currentState = dragableObjectState.DragCancelled;
        OnDragCancelled.Invoke();
        canReturn = true;

        if (changeTimeScale)
            Time.timeScale = 1f;
    }

    private void OnMouseDrag()
    {
        if (currentState == dragableObjectState.DragStarted)
        {
            //Updating object to mouse position while mouse is draging.
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        }
    }
    
    private void OnMouseEnter()
    {
        if(!canInteract)
            return;
        player.SetState(player.controllingState); //Set the player state to controlling so he can't move/attack.
    }

    private void OnMouseExit()
    {
        if(!canInteract)
            return;
        player.SetState(player.idleState);
    }
}
