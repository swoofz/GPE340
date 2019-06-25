using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup {

    [SerializeField] private Weapon weapon = null;

    protected override void OnPickUp(Player player) {
        player.Unequip();
        player.EquipWeapon(weapon);
        base.OnPickUp(player);
    }
}
