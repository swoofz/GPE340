using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StaminaBar : MonoBehaviour {

    public Text text = null;
    public Image fill = null;

    private Player target = null;

    void Update() {
        if (target)
            fill.fillAmount = target.StaminaPercentage;
    }

    public void SetTarget(Player player) {
        target = player;
    }
}
