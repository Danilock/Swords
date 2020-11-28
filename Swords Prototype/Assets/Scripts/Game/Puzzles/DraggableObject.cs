using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider2D), typeof(LerpMovement))]
public class DraggableObject : MonoBehaviour
{
    private PlayerController player;
    private LerpMovement lerpMovement;
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
        player = FindObjectOfType<PlayerController>();
        lerpMovement = GetComponent<LerpMovement>();
        lerpMovement.LerpSpeed = 200f;
        currentState = dragableObjectState.Idle;
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
    /// When left the mouse the object will return to its start position.
    /// </summary>
    private void OnMouseUp()
    {
        if(!canInteract)
            return;
        //If detects the Target Complete Area then set's the object state to completed.
        if (canCompleteDrag)
        {
            if (Physics2D.IsTouching(dragableObjectCollider, completeTargetArea))
            {
                OnDragCompleted.Invoke();
                lerpMovement.Move(completeTargetArea.transform);
                currentState = dragableObjectState.DragCompleted;
                canInteract = false;
                gameObject.layer = LayerMask.NameToLayer("Environment");
                player.SetState(player.idleState);
                return;
            }
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
        if(!canInteract || player.currentState == player.attackState)
            return;
        player.SetState(player.controllingState); //Set the player state to controlling so he can't move/attack.
    }

    private void OnMouseExit()
    {
        if(!canInteract)
            return;
        player.SetState(player.idleState);
    }

    public void GenerateCompleteArea()
    {
        GameObject newTargetArea = new GameObject(gameObject.name + " target area");
        newTargetArea.transform.position = transform.position;
        newTargetArea.layer = LayerMask.NameToLayer("No Ground");

        BoxCollider2D newTargetAreaCollider = newTargetArea.AddComponent<BoxCollider2D>();
        SpriteRenderer newTargetSprite = newTargetArea.AddComponent<SpriteRenderer>();
        newTargetSprite.sprite = GetComponent<SpriteRenderer>().sprite;
        newTargetSprite.sortingLayerID = GetComponent<SpriteRenderer>().sortingLayerID;
        
        newTargetAreaCollider.isTrigger = true;
        newTargetAreaCollider.size = new Vector2(.5f, .5f);
        completeTargetArea = newTargetAreaCollider;
        
        Rigidbody2D newTargetAreaRGB = newTargetArea.AddComponent<Rigidbody2D>();
        newTargetAreaRGB.isKinematic = true;
    }
}
