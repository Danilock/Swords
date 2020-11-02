using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LerpMovement : MonoBehaviour
{
    #region Events
    [SerializeField] UnityEvent OnReachEndPosition;
    #endregion
    #region Lerp Behaviour
    Vector2 startPosition, endPosition, onLevelStartPosition;
    [SerializeField, Range(0, 1)] float lerpPct; //Percentage of lerping(0 to 1).
    [SerializeField, Range(1, 100)] float lerpSpeed = 1;
    [SerializeField] bool move;
    [SerializeField] Collider2D objCollider;
    bool moving;
    #endregion 
    private void Start()
    {
        onLevelStartPosition = transform.position;
        objCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (moving)
        {
            lerpPct += 0.01f * Time.deltaTime * lerpSpeed;
            transform.position = Vector2.Lerp(startPosition, endPosition, lerpPct);

            if(Vector2.Distance(transform.position, endPosition) < .01f)
            {
                ObjectColliderTriggerState(false);
                moving = false;
                OnReachEndPosition.Invoke();
            }
        }
    }

    /// <summary>
    /// Moves the object to destination.
    /// </summary>
    /// <param name="destination"></param>
    public void Move(Transform destination)
    {
        lerpPct = 0f;
        startPosition = transform.position;
        endPosition = destination.position;
        moving = true;

        ObjectColliderTriggerState(true);
    }
    /// <summary>
    /// Moves the object to the initial position when it's instantiated or level loaded.
    /// </summary>
    public void MoveToInitialPosition()
    {
        lerpPct = 0f;
        startPosition = transform.position;
        endPosition = onLevelStartPosition;
        moving = true;

        ObjectColliderTriggerState(true);
    }

    ///Puts object collider to true(If it exist)
    void ObjectColliderTriggerState(bool state)
    {
        if (objCollider)
            objCollider.isTrigger = state;
    }
}
