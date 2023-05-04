using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveModifier : MonoBehaviour, ISubscriber
{
    public Vector2 newDirection;
    public bool activated;
   
    public float timeToWaitBeforeChange;
    private Animator anim;



    private void SetUpAnimator()
    {
       if(newDirection == Vector2.right)
        {
            anim.SetBool("Right", true);
        }
        else if (newDirection == Vector2.left)
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

    public void OnNotify(object content)
    {
        if(content is Player && !activated)
        {
            Invoke(nameof(SendMessage), timeToWaitBeforeChange);
            
        }
        if (content is PlayerStateBoomerangMovement)
        {
            activated= false;
        }
    }

    private void Start()
    {
      PubSub.Instance.RegisteredSubscriber(nameof(CurveModifier),this);
        activated= false;
        anim = GetComponent<Animator>();
        SetUpAnimator();

    }
    public void SendMessage()
    {
        PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);
    }
}
