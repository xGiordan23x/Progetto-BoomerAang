using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : Interactable
{
    [SerializeField] bool isOpen;
    [SerializeField] bool canOpenWithKey;

    [SerializeField] BoxCollider2D colliderImpatto;

    private Animator animator;
    [Header("Audio")]
    AudioClip ClipApriPorta;
    AudioClip ClipChiudiPorta;
    AudioClip ClipUtilizzoChiave;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (isOpen)
        {
            //DisableWallDoor();

            OpenDoor();

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
                    isOpen = true;


                    //Attivo audio utilizzo chiave
                    PlayAudioClipUtilizzaChiave();          


                    //animazione

                    player.SetCanMove(0);
                    player.animator.SetTrigger("OpenDoor");
                    OpenDoor();
                    
                    

                }

            }

        }

    }

   

    private void OnTriggerEnter2D(Collider2D collision)         //non utilizzato al momento
    {
        if (collision.GetComponent<Player>() != null && isOpen)
        {
            //apro la porta
            OpenDoor();

            Debug.Log("sono a portata");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)      //non utilizzato al momento
    {
        if (collision.GetComponent<Player>() != null && isOpen)
        {
            //chiudo la porta
            CloseDoor();

            Debug.Log("sono uscito");
        }
    }

    public void DisableWallDoor()       //solo unity animation event
    {
        if (colliderImpatto.enabled == true)
        {
            colliderImpatto.enabled = false;

            Debug.Log("disabilito");
        }

    }

    private void ActivateWall()     
    {
        if (colliderImpatto.enabled == false)
        {
            colliderImpatto.enabled = true;
        }
    }

    public void DisableDoor()           //da usare su un evento se non puo essere chiusa da una chiave;
    {
        if (canOpenWithKey)
        {
            return;
        }

        isOpen = false;
        ActivateWall();
        CloseDoor(); //momentaneo
    }

    public void AbilitateDoor()         //da usare su un evento se non puo essere aperto da una chiave;
    {
        if (canOpenWithKey)
        {
            return;
        }

        isOpen = true;

        OpenDoor(); //momentaneo

    }

    private void OpenDoor()
    {
        //animazione aprimento porta
        animator.SetBool("Active", true);

    }

    private void CloseDoor()
    {
        //animazione chiusura porta
        animator.SetBool("Active", false);

    }

    public void PlayAudioClipApriPorta()
    {
        AudioManager.instance.PlayAduioClip(ClipApriPorta);
    }
    public void PlayAudioClipChiudiPorta()
    {
        AudioManager.instance.PlayAduioClip(ClipChiudiPorta);
    }
    public void PlayAudioClipUtilizzaChiave()
    {
        AudioManager.instance.PlayAduioClip(ClipUtilizzoChiave);
    }

}
