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
    bool isDesactivated = false, collidedWithPlayer;

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && collidedWithPlayer && !isDesactivated)
        {
            isDesactivated = true;
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
        isDesactivated = false;
    }

    #region Physics
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
