using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SwipeMecLast
{
    [SerializeField] float forwardSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * forwardSpeed;
    }
}
