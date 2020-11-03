using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public class Event_OnStart : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnStartEvent;

        private void Start()
        {
            OnStartEvent.Invoke();
        }
    }
}