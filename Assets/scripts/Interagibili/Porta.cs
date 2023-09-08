using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PianoPorta
{
    Piano_1,
    Piano_2,
    Piano_3
}
public class Porta : Interactable
{
    [SerializeField] PianoPorta pianoPorta;
    [Header("Tipologia apertura porta")]
    [SerializeField] bool isOpen;
    [SerializeField] bool canOpenWithKey;
    [SerializeField] bool proximityOpen;

    [SerializeField] BoxCollider2D colliderImpatto;

    private Animator animator;
    [Header("Audio")]
    [SerializeField] AudioClip ClipApriPorta;
    [SerializeField] AudioClip ClipChiudiPorta;
    [SerializeField] AudioClip ClipUtilizzoChiave;
    [SerializeField] AudioClip ClipInterazionePortaChiusa;


    private void Start()
    {

        animator = GetComponent<Animator>();

        switch (pianoPorta)
        {
            case PianoPorta.Piano_1:
                animator.SetInteger("StanzaPiano", 0);
                break;
            case PianoPorta.Piano_2:
                animator.SetInteger("StanzaPiano", 1);
                break;
            case PianoPorta.Piano_3:
                animator.SetInteger("StanzaPiano", 2);
                break;
        }

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
                PlayAudioClipInterazionePortaChiusa();
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

                    if (player.stateMachine.GetCurrentState() is PlayerStateHumanMovement)
                    {
                        player.SetCanMove(0);
                        player.animator.SetTrigger("OpenDoor");
                    }

                    if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangMovement)
                    {
                        player.SetCanMove(0);
                        player.animator.SetTrigger("BoomerangInteract");
                    }


                    OpenDoor();




                }
                else
                {
                    Debug.Log("Non posso aprire la porta senza chiave");
                    PlayAudioClipInterazionePortaChiusa();
                }

            }

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
        AudioManager.instance.PlayAudioClip(ClipApriPorta);
    }
    public void PlayAudioClipChiudiPorta()
    {
        AudioManager.instance.PlayAudioClip(ClipChiudiPorta);
    }
    public void PlayAudioClipUtilizzaChiave()
    {
        AudioManager.instance.PlayAudioClip(ClipUtilizzoChiave);
    }
    public void PlayAudioClipInterazionePortaChiusa()
    {
        AudioManager.instance.PlayAudioClip(ClipInterazionePortaChiusa);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null && isOpen==false && proximityOpen)
        {
            OpenDoor();
            isOpen = true;
        }
    }

}
