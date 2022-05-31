using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public static class ExtansionDOTween
{
    public static void MyDOLocalMove(this Transform trs, Vector3 pos, float time = .15f, Action act = null) => 
        trs.DOLocalMove(pos, time).SetEase(Ease.Linear).OnComplete(() => act());

    public static void MyDOLocalJump(this Transform trs, Vector3 pos, float jumpPower = .8f, int numJumps = 1, float time = .2f) => 
        trs.DOLocalJump(pos, jumpPower, numJumps, time).SetEase(Ease.Linear);
}
