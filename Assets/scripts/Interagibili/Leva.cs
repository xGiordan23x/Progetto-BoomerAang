using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Leva : Interactable
{
    //[SerializeField] bool oneTime;
    private bool active;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    [Header("Audio")]
    AudioClip ClipAttivaLeva;

    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangReturning)
        {
            if (active)
            {
                Debug.Log("l'ho gia usata");
                return;
            }

            base.Interact(player);
            active = true;
            animator.SetTrigger("Pulled");
        }
        else
        {
            Debug.Log("non ci posso interagire");
        }

        

    }
    public void PlayAudioClipAttivaLeva()
    {
        AudioManager.instance.PlayAduioClip(ClipAttivaLeva);
    }
}
