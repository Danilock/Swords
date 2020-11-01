using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] bool canBeReactivated;
    [SerializeField] float timeToReactivate = 2f;
    [SerializeField] UnityEvent OnInteractWithPlayer;
    #region Can Interact
    [SerializeField] bool canInteract = true, collidedWithPlayer;
    public bool CanInteract { get { return canInteract; } set { canInteract = value; } }
    #endregion
    private void Update()
    {
        if (Input.GetButtonDown("Interact") && collidedWithPlayer && CanInteract)
        {
            canInteract = false;
            if (canBeReactivated)
            {
                StartCoroutine(Reactivate());
            }
            OnInteractWithPlayer.Invoke();
        }
    }

    IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(timeToReactivate);
        CanInteract = false;
    }
    #region Physics with player interactions
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            collidedWithPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            collidedWithPlayer = false;
        }
    }
    #endregion
}
