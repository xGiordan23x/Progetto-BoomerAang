using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : Interactable, ISubscriber
{
    public Transform stopPosition;
    public Vector2 newDirection;
    public bool activated;
   
    public float timeToWaitBeforeChange;
    private Animator anim;
    private Player playerRef;


    [Header("Audio")]
    [SerializeField] AudioClip ClipBoosterTrema;
    [SerializeField] AudioClip ClipBoosterSpara;



    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(Booster), this);
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
        if (!activated)
        {
            playerRef = player;
            player.transform.position = stopPosition.position;
            player.lastDirection = newDirection;

            PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);
            activated = true;
            player.GetComponent<SpriteRenderer>().enabled= false;
            anim.SetTrigger("Shake");
            Invoke(nameof(CallShootAnimation), timeToWaitBeforeChange);
        }
      
    }

    public void OnNotify(object content, bool vero = false)
    {
      
        if (content is PlayerStateBoomerangMovement)
        {
            activated= false;
        }
    }

   public void CallShootAnimation()
    {
        anim.SetTrigger("Shoot");
    }
    public void ShootPlayer()
    {
        PubSub.Instance.SendMessageSubscriber(nameof(Player),this);
      
    }

    public void PlayAudioClipTrema()
    {
        AudioManager.instance.PlayAudioClip(ClipBoosterTrema);
    }
    public void PlayAudioClipSpara()
    {
        AudioManager.instance.PlayAudioClip(ClipBoosterSpara);
    }

}
