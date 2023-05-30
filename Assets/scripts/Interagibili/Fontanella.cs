using UnityEngine;

public class Fontanella : Interactable, ISubscriber
{
   
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
        PubSub.Instance.SendMessageSubscriber(nameof(Player), this);
    }
    private void Update()
    {

    }
    public void OnNotify(object content, bool vero = false)
    {
        if(content is PlayerStateBoomerangMovement)
        {
            activated = false;
        }
    }
}
