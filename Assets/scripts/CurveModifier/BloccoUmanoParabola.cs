using UnityEngine;

public class BloccoUmanoParabola : Interactable, ISubscriber
{

    public bool activated;
    public Transform stopPosition;

    private Animator anim;

    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(Booster), this);
        activated = false;
        anim = GetComponent<Animator>();

    }


    public override void Interact(Player player)
    {

        OnInteraction.Invoke();
        activated = true;


    }

    public void OnNotify(object content, bool vero = false)
    {
        if (content is PlayerStateBoomerangMovement)
        {
            activated = false;
        }
    }



    public void StartParabola()
    {
        PubSub.Instance.SendMessageSubscriber(nameof(Player), this);
        PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator), this);
        PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateHumanMovement), this);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>().stateMachine.GetCurrentState() is PlayerStateHumanMovement)
        {

            Interact(collision.GetComponent<Player>());
        }

    }

}
