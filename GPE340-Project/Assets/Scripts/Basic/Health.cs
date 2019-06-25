using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour {

    public float HealthPercentage { get { return health / maxHealth; } }
    public float health { get; private set; }

    [SerializeField] private float maxHealth = 100f;

    [SerializeField, Tooltip("Raised every time the object is healed.")]
    private UnityEvent onHeal = null;
    [SerializeField, Tooltip("Raised every time the object is healed.")]
    private UnityEvent onDamage = null;
    [SerializeField, Tooltip("Raised every time the object is healed.")]
    private UnityEvent onDie = null;

    private void Awake() {
        health = maxHealth;
    }

    public void Damage(float damage) {
        damage = Mathf.Max(damage, 0f);
        health = Mathf.Clamp(health - damage, 0f, maxHealth);
        onDamage.Invoke();

        if (health == 0f) {
            SendMessage("OnDie", SendMessageOptions.DontRequireReceiver);
            onDie.Invoke();
        }
    }

    public void Heal(float heal) {
        heal = Mathf.Max(heal, 0f);
        health = Mathf.Clamp(health + heal, 0f, maxHealth);
        onHeal.Invoke();
    }
}
