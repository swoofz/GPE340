using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup {

    [SerializeField] private float heal = 0f;

    protected override void OnPickUp(Player player) {
        player.Health.Heal(heal);
        base.OnPickUp(player);
    }
}
