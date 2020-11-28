using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Execute an Unity Event when time went to zero.
/// </summary>
public class Event_Timer : MonoBehaviour
{
    [SerializeField] float timeToWait;
    float currentTime;
    [SerializeField] UnityEvent OnTimeTick;
    enum ClockState { Stopped, Ticking };
    ClockState currentClockState;

    private void Start()
    {
        currentTime = timeToWait;
    }

    private void Update()
    {
        if(currentClockState == ClockState.Ticking)
        {
            currentTime -= 1 * Time.deltaTime;

            if(currentTime <= 0)
            {
                OnTimeTick.Invoke();
                currentClockState = ClockState.Stopped;
            }
        }
    }

    public void StartTicking() => currentClockState = ClockState.Ticking;
}
