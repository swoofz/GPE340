using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour {

    public float CurrentHealth { get { return health; } private set { health = value; } }
    public float HealthPercentage { get { return health / maxHealth; } }

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health = 100f;

    /*
    [SerializeField, Tooltip("Raised every time the object is healed.")]
    private UnityEvent onHeal;
    [SerializeField, Tooltip("Raised every time the object is healed.")]
    private UnityEvent onDamage;
    [SerializeField, Tooltip("Raised every time the object is healed.")]
    private UnityEvent onDie;
    */


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    
    public void Damage(float damage) {
        damage = Mathf.Max(damage, 0f);
        health = Mathf.Clamp(health - damage, 0f, maxHealth);
        //onDamage.Invoke();

        if (health == 0f) {
            SendMessage("OnDie", SendMessageOptions.DontRequireReceiver);
            //onDie.Invoke();
        }
    }

    public void Heal(float heal) {
        heal = Mathf.Max(heal, 0f);
        health = Mathf.Clamp(health + heal, 0f, maxHealth);
        //onHeal.Invoke();
    }

    void ChangeState(string msg) {
        // UI stuff?
        // TODO:: Write the change state method
        // taking damage / destoryed

        SendMessage(msg, SendMessageOptions.DontRequireReceiver);
    }
}
