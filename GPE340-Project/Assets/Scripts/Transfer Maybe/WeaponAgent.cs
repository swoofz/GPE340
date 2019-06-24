using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class WeaponAgent : MonoBehaviour {

    public Weapon deafultWeapon;
    public Transform attachmentPoint;

    private Weapon equippedWeapon;

    public Animator Animator { get; private set; }

    protected virtual void OnAnimatorIK() {
        if (!equippedWeapon) return;

        if(equippedWeapon.rightHandIKTarget) {
            Animator.SetIKPosition(AvatarIKGoal.RightHand, equippedWeapon.rightHandIKTarget.position);
            Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            Animator.SetIKRotation(AvatarIKGoal.RightHand, equippedWeapon.rightHandIKTarget.rotation);
            Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        } else {
            Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }

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
        equippedWeapon = Instantiate(weapon) as Weapon;
        equippedWeapon.transform.SetParent(attachmentPoint);
        equippedWeapon.transform.localPosition = weapon.transform.localPosition;
        equippedWeapon.transform.localRotation = weapon.transform.localRotation;
        equippedWeapon.gameObject.layer = gameObject.layer;
    }

    public void Unequip() {
        if(equippedWeapon) {
            Destroy(equippedWeapon.gameObject);
            equippedWeapon = null;
        }
    }

    public void SetAwakeVaribles() {
        Animator = GetComponent<Animator>();
    }
}
