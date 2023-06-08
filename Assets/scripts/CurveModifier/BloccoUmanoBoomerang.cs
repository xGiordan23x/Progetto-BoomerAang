using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloccoUmanoBoomerang : Interactable, ISubscriber
{
    public bool activated;
    public Transform stopPosition;
    public bool shouldMovePlayer;
   
    private Animator anim;

    [Header("Audio")]
    AudioClip ClipRisucchio;
    AudioClip ClipRilascio;


    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(BloccoUmanoBoomerang), this);
        activated = false;
        anim = GetComponent<Animator>();
        if(shouldMovePlayer)
        {
            //set ventola
        }
        else
        {
            //setlaser
        }

    }


    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is PlayerStateHumanMovement)
        {


            OnInteraction.Invoke();
            activated = true;
            if (shouldMovePlayer)
            {
                player.transform.position = stopPosition.position;
            }
        }





    }


    public void TransformToBoomerang()
    {  
        if (shouldMovePlayer)
        {           
            PubSub.Instance.SendMessageSubscriber(nameof(Player), this, false);
        }

        else if (!shouldMovePlayer)
        {
           
            PubSub.Instance.SendMessageSubscriber(nameof(Player), this , true);
        }
           
            PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator), this);
            PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>().stateMachine.GetCurrentState() is PlayerStateHumanMovement)
        {
            Interact(collision.GetComponent<Player>());
        }

    }

    public void OnNotify(object content, bool vero = false)
    {
        if (content is PlayerStateBoomerangMovement)
        {
            activated = false;
        }
        if (content is BloccoStop)
        {

        }
    }

    public void PlayAudioClipRilascio()
    {
        AudioManager.instance.PlayAduioClip(ClipRilascio);
    }
    public void PlayAudioClipRisucchio()
    {
        AudioManager.instance.PlayAduioClip(ClipRisucchio);
    }
}
