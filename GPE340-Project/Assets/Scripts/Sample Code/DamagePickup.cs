using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : Pickup {
    [SerializeField, Tooltip("The amount of damage to apply to the player")]
    private float damage = 10f;

    protected override void OnPickUp(Player player) {
        // Damage Player
        player.Health.Damage(damage);
        base.OnPickUp(player);
    }
}
