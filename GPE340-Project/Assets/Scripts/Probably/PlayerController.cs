using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour {

    private Character character;    // Get our character methods

    private float health = 100f;
    public float Health { get { return health; } private set { health = value; } }

    void Awake() {
        character = GetComponent<Character>();      // Store our character component into a variable
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        character.Move();       // Move our player
        character.Rotate();     // Rotate our player

        // Sprint by pressing Left Shift and if our player has stamina to sprint
        if(Input.GetKey(KeyCode.LeftShift) && character.IsSprinting()) {
            character.Sprint();
        } else {    // Otherwise
            // Restore our player stamina
            character.RestoreStamina();
        }
    }
}
