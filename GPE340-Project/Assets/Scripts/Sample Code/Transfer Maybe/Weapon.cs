using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

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
}
