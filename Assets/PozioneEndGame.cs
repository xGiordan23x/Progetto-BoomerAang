using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PozioneEndGame : Interactable
{
    private bool taken = false;
    public override void Interact(Player player)
    {
        
        if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangMovement && taken!)
        {
            taken = true;
            base.Interact(player);
            

        }
        else
        {
            Debug.Log("non ci posso interagire");
        }



    }
}
