using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloccoUmanoBoomerang : Interactable, ISubscriber
{
    public bool activated;
    public Transform stopPosition;
    public bool muovePlayer;
   
    private Animator anim;

    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(BloccoUmanoBoomerang), this);
        activated = false;
        anim = GetComponent<Animator>();

    }


    public override void Interact(Player player)
    {
        if (muovePlayer)
        {
            player.transform.position = stopPosition.position;
        }
        OnInteraction.Invoke();
        activated = true;

        



    }

    public void OnNotify(object content)
    {

        if (content is PlayerStateBoomerangMovement)
        {
            activated = false;
        }
    }


    public void TransformToBoomerang()
    { 
  
            PubSub.Instance.SendMessageSubscriber(nameof(Player), this);
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
}
