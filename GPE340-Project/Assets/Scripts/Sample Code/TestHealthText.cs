using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TestHealthText : MonoBehaviour {

    public Health health;
    private Text text;

    private void Awake() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        text.text = string.Format("Health: {0}%", Mathf.RoundToInt(health.HealthPercentage * 100f));
    }
}
