using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    public float range;
    public LayerMask mask;

    public void Interaction()
    {
        Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position, range, mask);

        foreach (Collider2D coll in collision)
        {
            Interactable interactable = coll.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
