using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloccoStop :Interactable, ISubscriber
{
    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(BloccoStop), this);
    }
    public void OnNotify(object content)
    {
        
    }

    public override void Interact(Player player)
    {
       if(player.stateMachine.GetCurrentState() is PlayerStateBoomerangReturning)
        {
            PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);
        }
    }
}
