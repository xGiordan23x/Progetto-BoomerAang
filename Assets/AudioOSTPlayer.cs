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
            if(collision.gameObject.GetComponent<Player>().stateMachine.GetCurrentState() is PlayerStateBoomerangMovement || collision.gameObject.GetComponent<Player>().stateMachine.GetCurrentState() is PlayerStateHumanMovement)
            {
                onTriggerEnter.Invoke();
                
                GetComponent<BoxCollider2D>().enabled = false;
            }
            
        }
        

    }

    public void ResetVariables()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<AudioSource>().Stop();
    }
}
