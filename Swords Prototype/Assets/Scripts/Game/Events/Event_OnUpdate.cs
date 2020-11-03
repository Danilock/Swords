using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public class Event_OnUpdate : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnUpdateEvent;

        private void Update()
        {
            OnUpdateEvent.Invoke();
        }
    }
}