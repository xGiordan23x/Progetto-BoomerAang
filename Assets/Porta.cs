using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : Interactable
{
    [SerializeField] bool isOpen;
    [SerializeField] bool canOpenWithKey;

    [SerializeField] BoxCollider2D colliderImpatto;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (isOpen)
        {
            DisableWallDoor();
        }
    }
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null && isOpen)
        {
            //chiudo la porta
            CloseDoor();

        }
    }

    public void DisableWallDoor()
    {
        colliderImpatto.enabled = false;
    }

    public void AbilitateDoor()         //da usare su un evento se non puo essere aperto da una chiave;
    {
        if (canOpenWithKey)
        {
            return;
        }

        isOpen = true;
        
    }

    private void OpenDoor()
    {
        //animazione aprimento porta
        animator.SetBool("Active", true);

    }

    private void CloseDoor()
    {
        animator.SetBool("Active", false);
    }

}
