using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    private InterractUI boxTest;

    private void Start()
    {
        boxTest = FindAnyObjectByType<InterractUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            boxTest.ActivateBox();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            boxTest.DisabilitateBox();
        }
    }
}
