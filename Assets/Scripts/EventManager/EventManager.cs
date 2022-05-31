using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton

    public static EventManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public delegate void StartHandler();
    public delegate void FailHandler();
    public delegate void SuccessHandler();

    public delegate void NextHandler();
    public delegate void RetryHandler();

    public event StartHandler startEvent;
    public event FailHandler failEvent;
    public event SuccessHandler successEvent;

    public event NextHandler nextEvent;
    public event RetryHandler retryEvent;

    #region EventAwakes
    public void AwakeStartEvent() => startEvent();
    public void AwakeFailEvent() => failEvent();
    public void AwakeSuccessEvent() => successEvent();
    public void AwakeNextEvent() => nextEvent();
    public void AwakeRetryEvent() => retryEvent();
    #endregion
}
