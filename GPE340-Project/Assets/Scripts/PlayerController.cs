using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour {
    
    [SerializeField, Tooltip("The max speed of the player")]
    private float speed = 4f;

    private Animator animator;
    private Character character;

    void Awake() {
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    private void Move() {
        if (animator == null) return;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        input *= speed;
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.z);

        character.Move(input.x, input.z);
    }
}
