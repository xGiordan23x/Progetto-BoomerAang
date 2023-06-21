using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioOSTPlayer : MonoBehaviour
{
    public UnityEvent onTriggerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            if(collision.gameObject.GetComponent<Player>().stateMachine.GetCurrentState() is PlayerStateBoomerangMovement)
            {
                onTriggerEnter.Invoke();
                GetComponent<AudioOSTPlayer>().enabled = false;
            }
            
        }
        

    }
}
