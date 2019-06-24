using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon {

    [Header("Weapon Stats")]
    public float muzzleVelocity = 5f;
    public float shotsPerMinute = 900f;
    public float spread = 5f;

    private float timeNextShotIsReady;

    private void Awake() {
        timeNextShotIsReady = Time.time;
    }

    private void FixedUpdate() {
        if (Input.GetMouseButton(0)) {
            PullTrigger();
        } else {
            ReleaseTrigger();
        }

        if(triggerPulled) {
            while (Time.time > timeNextShotIsReady) {
                Shoot();
                timeNextShotIsReady += 60f / shotsPerMinute;
            }
        } else if (Time.time > timeNextShotIsReady) {
            timeNextShotIsReady = Time.time;
        }
    }

    void Shoot() {
        Projectile projectile = Instantiate(bullet, barrel.position, barrel.rotation * Quaternion.Euler(Random.onUnitSphere * spread)) as Projectile;
        projectile.damage = damage;
        projectile.rigidBody.AddRelativeForce(Vector3.forward * muzzleVelocity, ForceMode.VelocityChange);
        projectile.gameObject.layer = gameObject.layer;
    }

    public override void PullTrigger() {
        triggerPulled = true;
    }

    public override void ReleaseTrigger() {
        triggerPulled = false;
    }
}
