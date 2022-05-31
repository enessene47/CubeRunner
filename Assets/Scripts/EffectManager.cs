using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    #region Singleton

    public static EffectManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    #endregion


    [SerializeField] GameObject starExplosionEffectPrefab;

    [SerializeField] int starExplosionEffectsCount;

    ParticleSystem[] starExplosionEffects;

    int startEffectCounter;


    private void Start()
    {
        starExplosionEffects = new ParticleSystem[starExplosionEffectsCount];

        startEffectCounter = 0;

        for (int i = 0; i < starExplosionEffectsCount; i++)
        {
            var obj = Instantiate(starExplosionEffectPrefab, Vector3.zero, Quaternion.identity, transform);

            starExplosionEffects[i] = obj.GetComponent<ParticleSystem>();

            obj.hideFlags = HideFlags.HideInHierarchy;
        }
    }


    public void ActiveStarExplosion(Vector3 pos)
    {
        starExplosionEffects[startEffectCounter].transform.position = pos;
        starExplosionEffects[startEffectCounter].Play();

        startEffectCounter++;

        if (startEffectCounter == starExplosionEffectsCount)
            startEffectCounter = 0;
    }
}
