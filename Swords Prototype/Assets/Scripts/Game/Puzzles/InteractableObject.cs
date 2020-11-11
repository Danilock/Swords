using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] bool canBeReactivated;
    [SerializeField] float timeToReactivate = 2f;

    #region Events
    [SerializeField] UnityEvent OnInteractWithPlayer;

    public delegate void OnInteractingAction();

    public event OnInteractingAction OnInteracting;

    public delegate void OnStopInteracting();

    public event OnStopInteracting OnStopInteraction;
    #endregion
    #region Can Interact
    [SerializeField] bool canInteract = true, collidedWithPlayer;
    public bool CanInteract { get { return canInteract; } set { canInteract = value; } }
    #endregion
    public float Interacting { get; private set; }
    private void Update()
    {
        if (Input.GetButton("Interact") && collidedWithPlayer && CanInteract)
        {
            Interacting += 0.9f * Time.deltaTime;
            if (Interacting >= 1f)
            {
                Interacting = 0f;
                canInteract = false;
                if (canBeReactivated)
                {
                    StartCoroutine(Reactivate());
                }

                OnInteractWithPlayer.Invoke();
            }

            else
            {
                if(OnInteracting == null)
                    return;
                OnInteracting();
            }
        }
        else if (Input.GetButtonUp("Interact"))
        {
            Interacting = 0f;
            if(OnStopInteraction == null)
                return;
            OnStopInteraction();
        }
    }

    IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(timeToReactivate);
        CanInteract = true;
    }
    #region Physics with player interactions
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            collidedWithPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collidedWithPlayer = false;
        }
    }
    #endregion
}
