using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class CameraUI : MonoBehaviour {

    public Player player;

    [SerializeField, Tooltip("Update our UI Text to match Player stats.")]
    private UnityEvent UIUpdate = null;


    // Update is called once per frame
    void Update() {
        UIUpdate.Invoke();
    }

    public void HealthText(Text health) {
        health.text = string.Format("Health: {0}%", Mathf.RoundToInt(player.Health.HealthPercentage * 100f));
    } 

    public void StaminaText(Text stamina) {
        stamina.text = string.Format("Stamina: {0}%", Mathf.RoundToInt(player.StaminaPercentage * 100f));
    }
}
