using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TutorialType
{
    boomerang
}

public class TutorialDisplay : Interactable
{
    [SerializeField] TutorialType tutorialType;

    [SerializeField] Animator animator;

    [SerializeField] GameObject TutorialScreen;

    [SerializeField] bool display = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if(Input.GetButtonDown("Use") && display)
        {
            
            
               
            
            



        }
    }

    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is not PlayerStateBoomerangReturning && !display)
        {
            TutorialScreen.SetActive(true);
            display = true;
            player.SetCanMove(0);
            animator.SetBool("Display", true);
            animator.SetTrigger(tutorialType.ToString());


            Debug.Log("sentiamo");
        }
        else if (player.stateMachine.GetCurrentState() is not PlayerStateBoomerangReturning && display)
        {
            display = false;
            animator.SetBool("Display", false);
            PubSub.Instance.SendMessageSubscriber(nameof(Player), this);
            TutorialScreen.SetActive(false);

            Debug.Log("non sentiamo");
        }
    }
}
