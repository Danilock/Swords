using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class Event_OnTriggerEnter2D : MonoBehaviour
{
    [SerializeField] string colliderTag;
    [SerializeField] UnityEvent OnTriggerEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == colliderTag)
        {
            OnTriggerEvent.Invoke();
        }
    }
}
