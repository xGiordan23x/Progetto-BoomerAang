using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Leggibile : Interactable
{
    Animator animator;
    private void Start()
    {
        if (GetComponent<Animator>())
        {
            animator = GetComponentInChildren<Animator>();
        }
        else
        {
            animator = null;
        }
    }
    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is not PlayerStateBoomerangReturning)
        {
            base.Interact(player);
        }
    }
}
