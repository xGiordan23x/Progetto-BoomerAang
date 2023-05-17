using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloccoParabola : Interactable, ISubscriber
{
    public bool activated;
    public Transform stopPosition;
    public bool shouldMovePlayer;


    private Animator anim;

    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(BloccoParabola), this);
        activated = false;
        anim = GetComponent<Animator>();

    }


    public override void Interact(Player player)
    {
        if (shouldMovePlayer)
        {
            player.transform.position = stopPosition.position;
        }
        PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);
        activated = true;

        SendMessage();


    }

    public void OnNotify(object content)
    {

        if (content is PlayerStateBoomerangMovement)
        {
            activated = false;
        }
    }


    public void SendMessage()
    {
        PubSub.Instance.SendMessageSubscriber(nameof(Player), this);
        PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator), this);
    }
}
