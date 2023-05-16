using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Leva : Interactable
{
    public override void Interact(Player player)
    {
        if(player.stateMachine.GetCurrentState() is PlayerStateBoomerangReturning)
        {
            base.Interact(player);
        }
        
    }
}
