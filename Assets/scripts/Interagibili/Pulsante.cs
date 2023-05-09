using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsante : Interactable
{
    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is  not PlayerStateBoomerangReturning)    //il giocatore deve essere umano o boomerang che cammina
        {
            base.Interact(player);
        }

    }
}
