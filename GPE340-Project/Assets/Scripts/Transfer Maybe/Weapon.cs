using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour {

    [HideInInspector] public bool triggerPulled = false;

    public enum WeaonAnimationType {
        None = 0,
        Rifle = 1,
        Handgun = 2
    }

    [SerializeField, Header("Weapon Settings")]
    private WeaonAnimationType animationType = WeaonAnimationType.None;
    public float damage = 5f;

    [Header("IK Settings")]
    public Transform rightHandIKTarget;
    public Transform LeftHandIKTarget;

    [Header("Projectile Settings")]
    public Projectile bullet;
    public Transform barrel;



    public abstract void PullTrigger();
    public abstract void ReleaseTrigger();
}
