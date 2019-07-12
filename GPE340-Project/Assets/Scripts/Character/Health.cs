using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour {

    public float HealthPercentage { get { return health / maxHealth; } }
    public float health { get; private set; }
    public float MaxHealth { get { return maxHealth; } }
    public UnityEvent OnDie { get { return onDie; } }

    [SerializeField] private float maxHealth = 100f;

    [SerializeField, Tooltip("Raised every time the object is healed.")]
    private UnityEvent onHeal = null;
    [SerializeField, Tooltip("Raised every time the object is healed.")]
    private UnityEvent onDamage = null;
    [SerializeField, Tooltip("Raised every time the object is healed.")]
    private UnityEvent onDie = null;

    private AudioSource audioSource = null;

    private void Awake() {
        // Initailize Variables
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void Damage(float damage) {
        // Make sure the damage passed in is position, then take away health and make sure it is between max and 0
        damage = Mathf.Max(damage, 0f);
        health = Mathf.Clamp(health - damage, 0f, maxHealth);

        // Invoke any other things we need to do when damaged
        onDamage.Invoke();

        if (health == 0f) { // if dead
            SendMessage("OnDie", SendMessageOptions.DontRequireReceiver);
            onDie.Invoke();
        }
    }

    public void Heal(float heal) {
        // Make sure heal is position and add to the health between 0 and max
        heal = Mathf.Max(heal, 0f);
        health = Mathf.Clamp(health + heal, 0f, maxHealth);
        onHeal.Invoke();
    }

    public void PlayAudioClip(AudioClip clip) {
        if(audioSource)
            audioSource.PlayOneShot(clip);
    }
}
