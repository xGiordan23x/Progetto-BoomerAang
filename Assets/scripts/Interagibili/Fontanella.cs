using UnityEngine;

public class Fontanella : Interactable, ISubscriber
{

    public bool activated;

    public int timerToAdd;

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
            UseFountain();

            player.GetPotionGenerator().IncreaseTimerFontanella(timerToAdd);

        }

    }

    public void UseFountain()
    {
        activated = true;
        PubSub.Instance.SendMessageSubscriber(nameof(Player), this);

        //PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator), this);
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
