using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon {

    // More Weapon Stats
    public float muzzleVelocity = 5f;       // Forces the bullet fire at
    public float shotsPerMinute = 900f;     // Shots per minute
    public float spread = 5f;               // bullet spread

    private float timeNextShotIsReady;      // time to be able to take a shot again

    private void Awake() {
        // Initial Variables
        timeNextShotIsReady = Time.time;
    }

    private void FixedUpdate() {
        // Mouse button 0 is our shoot button
        if (Input.GetMouseButton(0)) {
            PullTrigger();
        } else {
            ReleaseTrigger();
        }

        if(triggerPulled) {
            while (Time.time > timeNextShotIsReady) {           // Can shoot?
                Shoot();                                        // Then shoot
                timeNextShotIsReady += 60f / shotsPerMinute;    // ask if can shoot again
            }
        } else if (Time.time > timeNextShotIsReady) {
            timeNextShotIsReady = Time.time;            // update can shoot while not ready
        }
    }

    void Shoot() {
        // Create or find a storage location for our bullets
        GameObject storage = GameObject.Find("Bullets");
        if(!storage) {
            storage = new GameObject("Bullets");
        }

        // Instantiate our bullet, add damage to it, add force to make it go forward, set it's layer to not collider with self, and
        //  set it parent transform for storage location to not populate the hierarchy to much
        Projectile projectile = Instantiate(bullet, barrel.position, barrel.rotation * Quaternion.Euler(Random.onUnitSphere * spread)) as Projectile;
        projectile.damage = damage;
        projectile.rigidBody.AddRelativeForce(Vector3.forward * muzzleVelocity, ForceMode.VelocityChange);
        projectile.gameObject.layer = gameObject.layer;
        projectile.transform.SetParent(storage.transform);
    }

    public override void PullTrigger() {
        // we pulled the trigger
        triggerPulled = true;
    }

    public override void ReleaseTrigger() {
        // we release the trigger
        triggerPulled = false;
    }
}
