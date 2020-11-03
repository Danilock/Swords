using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public class Event_OnTriggerExit2D : MonoBehaviour
    {
        [SerializeField] private string targetTag;
        [SerializeField] private UnityEvent onTriggerExit2D;

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(targetTag))
            {
                onTriggerExit2D.Invoke();
            }
        }
    }
}