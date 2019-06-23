using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour {

    public float stamina = 100f;            // Character stamina value to sprinting

    [HideInInspector] public float speed = 1f;          // Animator speed max value to get to... Change animations
    [HideInInspector] public bool moving = false;       // Whether or not our character is moving

}
