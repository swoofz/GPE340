using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Text text = null;                        // Health Text
    public Image fill = null;                       // Health Bar

    public bool destoryWithTarget = false;          
    public bool trackTarget = false;
    public Vector3 trackingOffset = Vector3.zero;

    private Health target;

    private void Update() {
        // Have target, update target's health bar
        if (target)
            fill.fillAmount = target.HealthPercentage;

        // Destoy this object when target dies
        if (destoryWithTarget && target.health == 0)
            Destroy(gameObject);

        // follow target
        if (trackTarget)
            transform.position = target.transform.position + trackingOffset;
    }

    public void SetTarget(Health character) {
        target = character;
    }
}
