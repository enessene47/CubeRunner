using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [Range(1, 3)]
    [SerializeField] int sizeCount = 1;

    BoxCollider coll;

    Transform[] myCubes;

    bool enter;

    void Start()
    {
        coll = GetComponent<BoxCollider>();

        myCubes = new Transform[sizeCount];

        for (int i = 0; i < sizeCount; i++)
        {
            var trs = CubeCreateManager.instance.CreateObstacleCube(transform);

            myCubes[i] = trs;

            trs.localPosition = new Vector3(0, .5f + i, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !enter)
        {
            enter = true;

            ObjectManager.instance.GetPlayerController.ObstacleInteraction(sizeCount);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && enter)
        {
            coll.enabled = false;

            ObjectManager.instance.GetPlayerController.AllCubeJump();
        }
    }
}
