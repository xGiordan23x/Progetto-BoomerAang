using System;
using System.Threading;
using UnityEngine;
using UnityEngine.WSA;

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
            player.animator.SetTrigger("InteractFountain");
            PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator), this);
            activated = true;
        }
        else
        {
            Debug.Log("Fontana disattivata");
        }
           
        
    }

   
    private void Update()
    {
       
    }
    public void OnNotify(object content)
    {

    }
}
