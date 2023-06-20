using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloccoStop :Interactable, ISubscriber
{
    
    public bool activated;
    public Transform stopPosition;
    public bool shouldMovePlayer;


    private Animator anim;
    [Header("Audio")]
    [SerializeField] AudioClip ClipRisucchio;
    [SerializeField] AudioClip ClipRilascio;


    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(Booster), this);
        activated = false;
        anim = GetComponent<Animator>();
        if (shouldMovePlayer)
        {
            //set blocco
        }
        else
        {
            //setlaser
        }


    }


    public override void Interact(Player player)
    {
       
        OnInteraction.Invoke();
        activated = true;
        if (shouldMovePlayer)
        {
            player.transform.position = stopPosition.position;
        }


    }

    public void OnNotify(object content, bool vero )
    {

        if (content is PlayerStateBoomerangMovement)
        {
            activated = false;
        }
        if(content is BloccoUmanoBoomerang)
        {

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

            PubSub.Instance.SendMessageSubscriber(nameof(Player),  this, true);
        }

      
        PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);

    }

    public void PlayAudioClipRilascio()
    {
        AudioManager.instance.PlayAudioClip(ClipRilascio);
    }
    public void PlayAudioClipRisucchio()
    {
        AudioManager.instance.PlayAudioClip(ClipRisucchio);
    }


}
