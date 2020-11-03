using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public class Event_OnCollisionExit2D : MonoBehaviour
    {
        [SerializeField] private string targetTag;
        [SerializeField] private UnityEvent OnCollisionExitEvent;

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                OnCollisionExitEvent.Invoke();
            }
        }
    }
}