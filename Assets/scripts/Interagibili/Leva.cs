using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Leva : Interactable, ISubscriber
{
    [SerializeField] Transform stopPosition;
    private bool active;
    private Player playerRef;

    private Animator animator;
    private void Awake()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(Leva),this);
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    [Header("Audio")]
    [SerializeField] AudioClip ClipAttivaLeva;

    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangReturning && !active)
        {
            if (active)
            {
                Debug.Log("l'ho gia usata");
                return;
            }

            base.Interact(player);
            active = true;
            animator.SetTrigger("Pulled");
        }
        if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangMovement && !active)
        {
            if (active)
            {
                Debug.Log("l'ho gia usata");
                return;
            }
            playerRef = player;
            base.Interact(player);
           
          

        }
        else
        {
            Debug.Log("non ci posso interagire");
        }

        

    }

    public void PlayerInteraction(Player player)
    {
        // Stop Timer
        PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator),this, true);
        //GetComponentInChildren<SpriteRenderer>().enabled= false;
        player.SetCanMove(0);
        player.transform.position = stopPosition.position;
        playerRef.GetComponent<SpriteRenderer>().enabled = false;

        animator.SetTrigger("playerPulled");

    }

     public void Continuetimer()
    {
        PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator), false);

    }
    public void PlayAudioClipAttivaLeva()
    {
        AudioManager.instance.PlayAudioClip(ClipAttivaLeva);
    }

    public void OnNotify(object content, bool vero = false)
    {
        throw new NotImplementedException();
    }

    public void ReEnablePlayer()
    {
        playerRef.GetComponent<SpriteRenderer>().enabled = true;


        playerRef.SetCanMove(1);
    }
    
}
