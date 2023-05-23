using UnityEngine;

public class Fontanella : Interactable, ISubscriber
{
    public float timerToAdd; 
    public bool activated;

    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(Fontanella), this);
        activated = false;
    }
    public override void Interact(Player player)
    {
        if (!activated && player.stateMachine.GetCurrentState() is PlayerStateHumanMovement)
        {
            base.Interact(player);
        
        }

    }

    public void UseFountain()
    {             
        activated = true;       
    }
    private void Update()
    {

    }
    public void OnNotify(object content)
    {
        if(content is PlayerStateBoomerangMovement)
        {
            activated = false;
        }
    }
}
