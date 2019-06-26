using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour {

    public bool triggerPulled { get; set; }
    public WeaonAnimationType AnimationType { get { return animationType; } }

    // Controler Animation type by what type of weapon we have
    public enum WeaonAnimationType {
        None = 0,
        Rifle = 1,
        Handgun = 2
    }

    [SerializeField, Header("Weapon Settings")]
    private WeaonAnimationType animationType = WeaonAnimationType.None;

    [Header("IK Settings")]
    public Transform rightHandIKTarget;
    public Transform LeftHandIKTarget;

    [Header("Projectile Settings")]
    public Projectile bullet;
    public Transform barrel;

    [Header("Weapon Stats")]
    public float damage = 5f;


    // Methods that need to be modified in Inheratid classes
    public abstract void PullTrigger();
    public abstract void ReleaseTrigger();
}
