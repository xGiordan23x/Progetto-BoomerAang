using UnityEngine;

public class ReadableObject : Interactable
{
    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is not PlayerStateBoomerangReturning)
        {
            base.Interact(player);
        }

        else
        {
            Debug.Log("Non ci posso interagire");
        }
    }
}