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

    [SerializeField] GameObject cubeCollectable;
    [SerializeField] GameObject cubeObstacle;

    public Transform CreateCollectableCube(Transform parentRef = null) => Instantiate(cubeCollectable, parent: parentRef).transform;
    public Transform CreateObstacleCube(Transform parentRef = null) => Instantiate(cubeObstacle, parent: parentRef).transform;
}
