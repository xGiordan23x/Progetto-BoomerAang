using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Computer : Interactable
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    [Header("Audio")]
    [SerializeField] AudioClip ClipTurnOn;
    [SerializeField] AudioClip ClipTurnOff;

    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is not PlayerStateBoomerangReturning)
        {
            animator.SetTrigger("On");
            Debug.Log("Ho Interagito con Boomerang");
            base.Interact(player);
        }
    }

    public void PlayAudioClipTurnOn()
    {
        AudioManager.instance.PlayAduioClip(ClipTurnOn);
    }

    public void PlayAudioClipTurnOff()
    {
        AudioManager.instance.PlayAduioClip(ClipTurnOff);
    }
}