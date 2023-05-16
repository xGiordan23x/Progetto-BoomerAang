using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : Interactable
{
    [SerializeField] bool isOpen;
    [SerializeField] bool canOpenWithKey;


    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is not PlayerStateBoomerangReturning && !isOpen)    //il giocatore deve essere umano o boomerang 
        {                                                                                               
            if (!canOpenWithKey)            //guarda se questa porta puo essere aperta da una chiave
            {
                Debug.Log("questa porta non puo essere aperta da una chiave");
                return;
            }
            
            Inventory inventory = player.GetComponent<Inventory>();

            if (inventory != null)
            {
                if (inventory.UseKey())
                {
                    AbilitateDoor();
                    OpenDoor();
                }

            }



        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null && isOpen)
        {
            //apro la porta
            OpenDoor();

        }
    }

    public void AbilitateDoor()         //da usare su un evento se non puo essere aperto da una chiave;
    {
        isOpen = true;
    }

    private void OpenDoor()
    {
        //animazione aprimento porta
        gameObject.SetActive(false);
    }

}
