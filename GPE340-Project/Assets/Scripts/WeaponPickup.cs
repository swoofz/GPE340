using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup {

    [SerializeField] private Weapon weapon = null;              // Storing the weapon for each pick with this component

    protected override void OnPickUp(Player player) {
        // Unequip any weapon that might be equipped then equip this weapon
        player.Unequip();
        player.EquipWeapon(weapon);
        base.OnPickUp(player);
    }
}
