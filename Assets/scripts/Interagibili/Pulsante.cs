using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsante : Interactable
{
    bool active;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is not PlayerStateBoomerangReturning && !active)    //il giocatore deve essere umano o boomerang che cammina
        {
            base.Interact(player);
            active = true;
            animator.SetTrigger("Pressed");

        }

    }


}
