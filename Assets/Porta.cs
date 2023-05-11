using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : Interactable
{
    [SerializeField] bool isOpen;

    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is not PlayerStateBoomerangReturning && !isOpen)    //il giocatore deve essere umano o boomerang che cammina
        {
            Inventory inventory = GetComponent<Inventory>();

            if(inventory != null)
            {
                if (inventory.UseKey())
                {
                    isOpen = true;
                }
                
            }


            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null && isOpen)
        {
            //apro la porta

            gameObject.SetActive(false);
        }
    }

}
