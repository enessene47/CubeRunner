using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCreateManager : MonoBehaviour
{
    #region Singleton

    public static CubeCreateManager instance;

    private void Awake()
    {
        if(instance == null) instance = this;
    }

    #endregion

    [SerializeField] GameObject cube;

    public Transform CreateCube(Transform parentRef = null) => Instantiate(cube, parent: parentRef).transform;
}
