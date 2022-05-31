using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    [SerializeField] GameObject mainCanvas;

    private void Start()
    {
        EventManager.instance.startEvent += () => mainCanvas.SetActive(false);
    }
}
