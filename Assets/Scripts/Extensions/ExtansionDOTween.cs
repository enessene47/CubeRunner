using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public static class ExtansionDOTween
{
    public static void MyDOLocalMove(this Transform trs, Vector3 pos, float time = .15f, Action act = null) => 
        trs.DOLocalMove(pos, time).SetEase(Ease.Linear).OnComplete(() => act());

    public static void MyDOLocalJump(this Transform trs, Vector3 pos) => trs.DOLocalJump(pos, .8f, 1, .2f).SetEase(Ease.Linear);
}
