using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveModifier : Interactable, ISubscriber
{
    public Transform stopPosition;
    public Vector2 newDirection;
    public bool activated;
   
    public float timeToWaitBeforeChange;
    private Animator anim;

    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(CurveModifier), this);
        activated = false;
        anim = GetComponent<Animator>();
        SetUpAnimator();

    }
   
    private void SetUpAnimator()
    {
       if(newDirection == Vector2.right)
        {
            anim.SetBool("Right", true);
        }
        if (newDirection == Vector2.left)
        {
            anim.SetBool("Left", true);
        }
        if (newDirection == Vector2.down)
        {
            anim.SetBool("Down", true);
        }
        if (newDirection == Vector2.up)
        {
            anim.SetBool("Up", true);
        }
    }
    public override void Interact(Player player)
    {
        player.transform.position = stopPosition.position;
        player.lastDirection = newDirection;    
        
        PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);
        activated = true;
       
        Invoke(nameof(SendMessage), timeToWaitBeforeChange);


    }

    public void OnNotify(object content, bool vero = false)
    {
      
        if (content is PlayerStateBoomerangMovement)
        {
            activated= false;
        }
    }

   
    public void SendMessage()
    {
        PubSub.Instance.SendMessageSubscriber(nameof(Player),this);
    }
}
