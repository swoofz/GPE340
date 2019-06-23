using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPickUp : Pickup {

    protected override void OnPickUp(Player player) {
        Debug.LogFormat("I've been picked up by {0}!", player);
        base.OnPickUp(player);
    }
}
