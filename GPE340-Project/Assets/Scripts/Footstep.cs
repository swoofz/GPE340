using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {

    [SerializeField, Tooltip("The auido source to play through")]
    private AudioSource audioSource = null;
    [SerializeField, Tooltip("The footstep sound")]
    private AudioClip footstepClip = null;

    private void AnimationEventFootstep() {
        audioSource.PlayOneShot(footstepClip);
    }

}
