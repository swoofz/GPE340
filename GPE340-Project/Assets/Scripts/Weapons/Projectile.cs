using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Rigidbody rigidBody { get; private set; }

    public float damage { get; set; }  // bullet damage

    [SerializeField] private float lifespan = 1.2f;         // Time to delay the destory 

    [Header("Hit Effect Settings")]
    [SerializeField] private ParticleSystem defaultHitEffect = null;
    [SerializeField] private float hitEffectLifespan = 1f;  // Time to delay the destory of the hit effect


    private void Awake() {
        // Initialize Variables
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        // Get health component on our collision target
        Health player = collision.gameObject.GetComponent<Health>();
        if(player != null) {        // if have a health component 
            player.Damage(damage);  // do damage
        }

        // Hit Effect
        ProjectileHitOverride hitOverride = collision.gameObject.GetComponent<ProjectileHitOverride>();
        //                            if hitOverride   use override effect   else   use defualt effect
        ParticleSystem hitEffect = Instantiate(hitOverride ? hitOverride.hitEffect : defaultHitEffect,
            collision.GetContact(0).point,
            Quaternion.Inverse(transform.rotation)) as ParticleSystem;
        Destroy(hitEffect.gameObject, hitEffectLifespan);



        Destroy(gameObject, lifespan);  // else destory after give time
    }
}
