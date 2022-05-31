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
    public delegate void RetryHandler();
    public delegate void NextHandler();

    public event StartHandler startEvent;
    public event RetryHandler failEvent;
    public event NextHandler successEvent;

    #region EventAwakes
    public void AwakeStartEvent() => startEvent();
    public void AwakeFailEvent() => failEvent();
    public void AwakeSuccessEvent() => successEvent();
    #endregion
}
