using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pedana : MonoBehaviour
{
    [SerializeField] UnityEvent pressIn;
    [SerializeField] UnityEvent pressOut;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Scatola>() != null)
        {
            pressIn?.Invoke();
        }

        if (collision.GetComponent<Player>() != null)
        {
            if(collision.GetComponent<Player>().stateMachine.GetCurrentState() is PlayerStateHumanMovement)
            {
                pressIn?.Invoke();
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
