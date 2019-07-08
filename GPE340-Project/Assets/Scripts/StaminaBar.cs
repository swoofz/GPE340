using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StaminaBar : MonoBehaviour {

    public Text text = null;            // Stamina Text
    public Image fill = null;           // Stamina Bar

    private Player target = null;

    void Update() {
        // Have target, update target's stamina bar
        if (target)
            fill.fillAmount = target.StaminaPercentage;
    }

    public void SetTarget(Player player) {
        target = player;
    }
}
