using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Text text = null;
    public Image fill = null;
    public bool destoryWithTarget = false;
    public bool trackTarget = false;
    public Vector3 trackingOffset = Vector3.zero;

    private Health target;

    private void Update() {
        if (target)
            fill.fillAmount = target.HealthPercentage;

        if (destoryWithTarget && target.health == 0)
            Destroy(gameObject);

        if (trackTarget)
            transform.position = target.transform.position + trackingOffset;
    }


    public void SetTarget(Health character) {
        target = character;
    }
}
