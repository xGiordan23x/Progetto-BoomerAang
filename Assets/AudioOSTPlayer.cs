using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioOSTPlayer : MonoBehaviour
{
    public UnityEvent onTriggerEnter;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>().stateMachine.GetCurrentState() is PlayerStateBoomerangMovement)
        {
            onTriggerEnter.Invoke();
            GetComponent<BoxCollider2D>().enabled = false;
        }
       
    }
}
