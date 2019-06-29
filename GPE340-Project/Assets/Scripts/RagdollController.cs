using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour {

    private Collider Collider;
    private Rigidbody rb;
    private WeaponAgent Character;

    private void Awake() {
        Collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        Character = GetComponent<WeaponAgent>();
    }


    public void TurnOffElementsIncludingChildren() {
        int i;
        Rigidbody[] childRBs = GetComponentsInChildren<Rigidbody>();
        for (i = 0; i < childRBs.Length; i++) {
            childRBs[i].isKinematic = true;
        }

        Collider[] childColliders = GetComponentsInChildren<Collider>();
        for (i = 0; i < childColliders.Length; i++) {
            childColliders[i].enabled = false;
        }

        rb.isKinematic = false;
        Collider.enabled = true;
        Character.Animator.enabled = true;
    }

    public void TurnOnElementsIncludingChildren() {
        int i;
        Rigidbody[] childRBs = GetComponentsInChildren<Rigidbody>();
        for (i = 0; i < childRBs.Length; i++) {
            childRBs[i].isKinematic = false;
        }

        Collider[] childColliders = GetComponentsInChildren<Collider>();
        for (i = 0; i < childColliders.Length; i++) {
            childColliders[i].enabled = true;
        }

        rb.isKinematic = true;
        Collider.enabled = false;
        Character.Animator.enabled = false;
    }
}
