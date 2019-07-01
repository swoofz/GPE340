using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour {

    private Collider Collider;      // Store our Collider
    private Rigidbody rb;           // Store our Rigidbody
    private WeaponAgent Character;  // Get our character

    private void Awake() {
        // Initialize variables
        Collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        Character = GetComponent<WeaponAgent>();
    }


    private void Update() {
        // Toggle Ragdoll
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            TurnOffElementsIncludingChildren();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            TurnOnElementsIncludingChildren();
        }
    }

    public void TurnOffElementsIncludingChildren() {
        int i;
        // Get all child rigidbody components
        Rigidbody[] childRBs = GetComponentsInChildren<Rigidbody>();
        for (i = 0; i < childRBs.Length; i++) {
            childRBs[i].isKinematic = true;     // Child element can be affect by physics
        }

        // Get all child Collider components
        Collider[] childColliders = GetComponentsInChildren<Collider>();
        for (i = 0; i < childColliders.Length; i++) {
            childColliders[i].enabled = false;  // Child element collider disable
        }

        // Disable character colliders and dont have physics affect it rigibody
        rb.isKinematic = false;
        Collider.enabled = true;
        Character.Animator.enabled = true;
    }

    public void TurnOnElementsIncludingChildren() {
        int i;
        // Get all child rigidbody components
        Rigidbody[] childRBs = GetComponentsInChildren<Rigidbody>();
        for (i = 0; i < childRBs.Length; i++) {
            childRBs[i].isKinematic = false;    // Child element can't be affect by physics
        }

        // Get all child Collider components
        Collider[] childColliders = GetComponentsInChildren<Collider>();
        for (i = 0; i < childColliders.Length; i++) {
            childColliders[i].enabled = true;   // Child element collider enable
        }

        // Activate character colliders and have physics affect it rigibody
        rb.isKinematic = true;
        Collider.enabled = false;
        Character.Animator.enabled = false;
    }
}
