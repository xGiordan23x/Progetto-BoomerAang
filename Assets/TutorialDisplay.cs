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
    [SerializeField] bool canInteract = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (canInteract)
        {
            if (Input.GetButtonDown("Use") && display)
            {
                DeactivateTutorialScreen();
            }
        }
    }


    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is not PlayerStateBoomerangReturning && !display)
        {
           ActivateTutorialScreen();
        }
       
    }


    public void ActivateTutorialScreen()
    {
        TutorialScreen.SetActive(true);
        display = true;
        PubSub.Instance.SendMessageSubscriber(nameof(Player), this, true);
        animator.SetBool("Display", true);
        animator.SetTrigger(tutorialType.ToString());
        //canInteract= true;
        Invoke(nameof(SetCanInteract), 1f);
        Debug.Log("Sentiamo");


    } 
    public void DeactivateTutorialScreen()
    {
        display = false;
        animator.SetBool("Display", false);
        PubSub.Instance.SendMessageSubscriber(nameof(Player), this, false);
        TutorialScreen.SetActive(false);
        canInteract= false;
        Debug.Log("non sentiamo");
    }
    public void SetCanInteract()
    {
        canInteract= true;
    }
}
