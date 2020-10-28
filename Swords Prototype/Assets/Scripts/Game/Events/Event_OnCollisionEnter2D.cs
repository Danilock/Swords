using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event_OnCollisionEnter2D : MonoBehaviour
{
    [SerializeField] string collisionTag;
    [SerializeField] UnityEvent OnCollisionEvent;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == collisionTag)
        {
            OnCollisionEvent.Invoke();
        }
    }
}
