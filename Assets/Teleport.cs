using UnityEngine;

public class Teleport : Interactable
{
    public Teleport teleportToReach;
    bool activated = false;
    public float cooldown;


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
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke(nameof(ResetTimer), cooldown);
    }
    public void ResetTimer()
    {
        activated= false;
        GetComponent<SpriteRenderer>().color= Color.green;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Interact(collision.GetComponent<Player>());
        }
        
    }
}
