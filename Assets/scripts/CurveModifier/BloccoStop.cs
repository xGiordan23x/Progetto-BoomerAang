using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloccoStop :Interactable, ISubscriber
{
    
    public bool activated;
    public Transform stopPosition;
    public bool shouldMovePlayer;


    private Animator anim;

    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(CurveModifier), this);
        activated = false;
        anim = GetComponent<Animator>();
       
    }

   
    public override void Interact(Player player)
    {
        if(shouldMovePlayer)
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
        PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);

    }
}
