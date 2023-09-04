using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Leva : Interactable, ISubscriber
{
    [SerializeField] Transform stopPosition;
    private bool active;

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
        else if (player.stateMachine.GetCurrentState() is PlayerStateHumanMovement && !active)
        {
            if (active)
            {
                Debug.Log("l'ho gia usata");
                return;
            }
            base.Interact(player);
            active = true;
            PLayInteraction(player);
        }
        else
        {
            Debug.Log("non ci posso interagire");
        }

        

    }

    private void PLayInteraction(Player player)
    {
        // Stop Timer
        PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator),this, true);

        player.SetCanMove(0);
        player.transform.position = stopPosition.position;
        player.GetComponent<SpriteRenderer>().enabled = false;
        animator.SetTrigger("playerPulled");

        //in animazione alla fine setCanMove(1)

      

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
}
