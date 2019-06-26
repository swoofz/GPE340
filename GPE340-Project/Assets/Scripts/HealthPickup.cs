using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup {

    [SerializeField] private float heal = 0f;   // health that will be restore

    protected override void OnPickUp(Player player) {
        // Restore health
        player.Health.Heal(heal);
        base.OnPickUp(player);
    }
}
