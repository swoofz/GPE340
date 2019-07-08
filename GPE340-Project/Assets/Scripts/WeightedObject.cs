using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeightedObject {

    public double Chance { get { return chance; } }

    [SerializeField, Tooltip("The object selected by this choice.")]
    private Object value = null;
    [SerializeField, Tooltip("The chance to select the value.")]
    private double chance = 1.0;

    static private System.Random rnd;


    static public Object Select(WeightedObject[] choices) {
        rnd = new System.Random();
        double[] cdfArray = new double[choices.Length];
        double weight = 0;

        // CDF - Cumulative Density Function
        for (int i = 0; i < choices.Length; i++) {
            weight += choices[i].chance;
            cdfArray[i] = weight;
        }

        // Find a random index between 0 and last weight
        int selectedIndex = System.Array.BinarySearch(cdfArray, rnd.NextDouble() * cdfArray[cdfArray.Length - 1]);
        if (selectedIndex < 0)
            selectedIndex = ~selectedIndex;

        // Return item
        return choices[selectedIndex].value;
    }

}
