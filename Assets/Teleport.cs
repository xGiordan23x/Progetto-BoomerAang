using UnityEngine;

public class Teleport : Interactable
{
    public Teleport teleportToReach;
    bool activated = false;
    public float cooldown;

    [SerializeField] bool isDxTeleport;
    Animator Animator;
    //[SerializeField] Color disableColor;

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
        GetComponentInChildren<SpriteRenderer>().color = Color.gray;
        Invoke(nameof(ResetTimer), cooldown);
    }
    public void ResetTimer()
    {
        activated= false;
        GetComponentInChildren<SpriteRenderer>().color= Color.white;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Interact(collision.GetComponent<Player>());
        }
        
    }
}
