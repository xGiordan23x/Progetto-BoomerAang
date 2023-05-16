using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloccoStop :Interactable, ISubscriber
{
    //private void Start()
    //{
    //    PubSub.Instance.RegisteredSubscriber(nameof(BloccoStop), this);
    //}
    //public void OnNotify(object content)
    //{

    //}

    //public override void Interact(Player player)
    //{
    //   if(player.stateMachine.GetCurrentState() is PlayerStateBoomerangReturning)
    //    {
    //        PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);
    //    }
    //}

   
    public bool activated;

    
    private Animator anim;

    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(CurveModifier), this);
        activated = false;
        anim = GetComponent<Animator>();
       
    }

   
    public override void Interact(Player player)
    {
       
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
    }
}
