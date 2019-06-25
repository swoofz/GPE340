using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class TestHealthText : MonoBehaviour {

    public Health health;
    private Text text = null;

    [SerializeField]
    private UnityEvent UIUpdate = null;

    private void Awake() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        UIUpdate.Invoke();
        text.text = string.Format("Health: {0}%", Mathf.RoundToInt(health.HealthPercentage * 100f));
    }

    public void Test() {
        Debug.Log("Test");
    }
}
