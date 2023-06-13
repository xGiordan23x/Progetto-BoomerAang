using UnityEngine;

public class Teleport : Interactable
{
    public Teleport teleportToReach;
    bool activated = false;
    public float cooldown;

    [SerializeField] bool isDxTeleport;
    Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();

        if (isDxTeleport)
        {
            Animator.SetBool("DX", true);
        }
    }

    public override void Interact(Player player)
    {
        if (!activated)
        {
            player.transform.position = teleportToReach.transform.position;
            teleportToReach.Activate();
            Activate();

        }
    }

    public void Activate()
    {
        activated = true;
        //GetComponentInChildren<SpriteRenderer>().color = Color.gray;
        Invoke(nameof(ResetTimer), cooldown);

        //animazione

        if (isDxTeleport)
        {
            Animator.SetBool("ActivationDX", true);
        }
        else
        {
            Animator.SetBool("ActivationSX", true);
        }




    }
    public void ResetTimer()
    {
        activated = false;
        GetComponentInChildren<SpriteRenderer>().color = Color.white;

        //animazione

        if (isDxTeleport)
        {
            Animator.SetBool("ActivationDX", false);
            Animator.SetTrigger("Activation");
        }
        else
        {
            Animator.SetBool("ActivationSX", false);
            Animator.SetTrigger("Activation");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Interact(collision.GetComponent<Player>());
        }

    }

    
}
