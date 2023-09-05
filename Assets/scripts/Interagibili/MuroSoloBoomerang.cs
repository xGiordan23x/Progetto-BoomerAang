using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroSoloBoomerang : Interactable
{
    public GameObject positionToReach;
    // Start is called before the first frame update
    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangMovement)
        {
            base.Interact(player);
            player.transform.position = positionToReach.transform.position;
        }

    }
}
