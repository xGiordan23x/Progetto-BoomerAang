using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Leva : Interactable
{
    [SerializeField] bool oneTime;
    private bool _onetime;

    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangReturning)
        {
            if (_onetime)
            {
                Debug.Log("l'ho gia usata");
                return;
            }

            base.Interact(player);
            _onetime = true;
        }
        else
        {
            Debug.Log("non ci posso interagire");
        }

        

    }
}
