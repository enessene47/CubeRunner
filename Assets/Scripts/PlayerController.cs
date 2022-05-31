using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SwipeMecLast
{
    [SerializeField] Transform character;

    [SerializeField] float forwardSpeed;

    Animator anim;

    List<Transform> collectCube;

    private enum State{Wait, Play, End};
    private enum AnimState{Happy, Sad, Idle};

    State state;

    AnimState animState;

    void Start()
    {
        base.BaseStart();

        collectCube = new List<Transform>();

        collectCube.Add(CubeCreateManager.instance.CreateCube(transform));

        FirstAdjustment();

        anim = GetComponent<Animator>();

        state = State.Play;

        animState = AnimState.Idle;
    }

    void Update()
    {
        if (state != State.Play)
            return;

        transform.position += Vector3.forward * Time.deltaTime * forwardSpeed;

        Swipe();
    }


    private void AnimationState()
    {
        switch(animState)
        {
            case AnimState.Idle: anim.SetTrigger("Idle"); break;
            case AnimState.Happy: anim.SetTrigger("Happy"); break;
            case AnimState.Sad: anim.SetTrigger("Sad"); break;
        }
    }

    private void TransformAdjustment()
    {
        int count = collectCube.Count;

        for (int i = 0; i < count; i++)
            collectCube[i].MyDOLocalMove(new Vector3(0, (count - i) * .5f, 0));

        character.MyDOLocalMove(new Vector3(0, count * .5f + .5f, 0), act: () => character.MyDOLocalJump(character.localPosition,
            Random.Range(.5f, .8f)));
    }

    private void FirstAdjustment()
    {
        collectCube[0].position = Vector3.up * .5f;

        character.position = Vector3.up;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cube"))
        {
            other.enabled = false;

            other.transform.parent = transform;

            collectCube.Add(other.transform);

            TransformAdjustment();
        }    
    }
}
