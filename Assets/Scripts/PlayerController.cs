using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SwipeMecLast
{
    [SerializeField] Transform character;

    [SerializeField] float forwardSpeed;

    [SerializeField] Animator anim;

    List<Transform> collectCube;

    private enum State{Wait, Play, End};
    private enum AnimState{Happy, Sad, Idle};

    State state;

    AnimState animState;

    void Start()
    {
        base.BaseStart();

        collectCube = new List<Transform>();

        collectCube.Add(CubeCreateManager.instance.CreateCollectableCube(transform));

        FirstAdjustment();

        EventManager.instance.startEvent += () => state = State.Play;

        EventManager.instance.failEvent += () =>
        {
            state = State.End;

            animState = AnimState.Sad;

            AnimationState();
        };

        EventManager.instance.successEvent += () =>
        {
            state = State.End;

            animState = AnimState.Happy;

            AnimationState();

            StartCoroutine(LevelEnd());
        };
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
            collectCube[i].MyDOLocalMove(new Vector3(0, (count - i - 1) + .5f, 0));

        character.MyDOLocalMove(new Vector3(0, count, 0), act: () => character.MyDOLocalJump(character.localPosition,
            Random.Range(.5f, .8f)));
    }

    public void AllCubeJump(float time = .45f)
    {
        int count = collectCube.Count;

        for (int i = 0; i < count; i++)
            collectCube[i].MyDOLocalMove(new Vector3(0, (count - i - 1) + .5f, 0));

        character.MyDOLocalMove(new Vector3(0, count, 0), act: () =>
        {
            for (int i = 0; i < count; i++)
                collectCube[i].MyDOLocalJump(collectCube[i].localPosition, jumpPower: (count - i) * .8f, time: time);

            character.MyDOLocalJump(character.localPosition,jumpPower: (count + 1) * .8f ,time: time
        );
    });
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
        else if(other.CompareTag("LevelEnd"))
        {
            other.enabled = false;

            EventManager.instance.AwakeSuccessEvent();
        }
    }

    public void ObstacleInteraction(int obsSize)
    {
        int collectCount = collectCube.Count;

        if (obsSize >= collectCount)
        {
            EventManager.instance.AwakeFailEvent();

            return;
        }

        for(int i = collectCount - 1; i >= collectCount - obsSize; i--)
        {
            var trs = collectCube[i];

            trs.parent = null;

            collectCube.Remove(trs);
        }
    }

    private IEnumerator LevelEnd()
    {
        for (int i = collectCube.Count - 1; i >= 0; i--)
        {
            UIManager.instance.UpdateCoinText(10);

            EffectManager.instance.ActiveStarExplosion(new Vector3(collectCube[i].position.x, collectCube[i].position.y, collectCube[i].position.z - .8f));

            yield return new WaitForSeconds(.4f);
        }

        AllCubeJump(.7f);
    }
}
