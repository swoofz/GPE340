using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitOverride : MonoBehaviour {
    public ParticleSystem hitEffect { get { return HitEffect; } }

    [SerializeField] private ParticleSystem HitEffect = null;
}
