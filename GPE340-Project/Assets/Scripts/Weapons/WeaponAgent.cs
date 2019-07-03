using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class WeaponAgent : MonoBehaviour {
    [Header("Weapon Setttings")]
    public Transform attachmentPoint;           // Place where we want our weapon to be located

    public Animator Animator { get; private set; }
    public Weapon equippedWeapon { get; private set; }

    protected virtual void Awake() {
        Animator = GetComponent<Animator>();
    }

    protected virtual void OnAnimatorIK() {
        // if dont have a weapon don't even worry about the rest of the code
        if (!equippedWeapon) return;

        // else set our right hand on the weapon in a spefic position that is set from the weapon
        if(equippedWeapon.rightHandIKTarget) {
            Animator.SetIKPosition(AvatarIKGoal.RightHand, equippedWeapon.rightHandIKTarget.position);      // set the position
            Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            Animator.SetIKRotation(AvatarIKGoal.RightHand, equippedWeapon.rightHandIKTarget.rotation);      // set the rotation
            Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        } else {
            Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }

        // Left hand --- same as above
        if (equippedWeapon.LeftHandIKTarget) {
            Animator.SetIKPosition(AvatarIKGoal.LeftHand, equippedWeapon.LeftHandIKTarget.position);
            Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            Animator.SetIKRotation(AvatarIKGoal.LeftHand, equippedWeapon.LeftHandIKTarget.rotation);
            Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        } else {
            Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
        }
    }

    public void EquipWeapon(Weapon weapon) {
        // Equip weapon
        equippedWeapon = Instantiate(weapon) as Weapon;                                                 // Instantiate weapon
        equippedWeapon.transform.SetParent(attachmentPoint);                                            // Set it parent transform / location it is in the hierarchy
        equippedWeapon.transform.localPosition = weapon.transform.localPosition;                        // Set it's position
        equippedWeapon.transform.localRotation = weapon.transform.localRotation;                        // Set it's rotation
        equippedWeapon.gameObject.layer = gameObject.layer;                                             // Set the layer it on
        Animator.SetInteger("Weapon Animation Type", equippedWeapon.AnimationType.GetHashCode());       // Set animitor to be on the right animation
    }

    public void Unequip() {
        // if have a weapon equip then unequiped it
        if(equippedWeapon) {
            Destroy(equippedWeapon.gameObject);
            equippedWeapon = null;
        }
    }
}
