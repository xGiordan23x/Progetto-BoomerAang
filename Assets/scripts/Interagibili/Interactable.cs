using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{

    public UnityEvent OnInteraction;

    public virtual void Interact(Player player)
    {
        OnInteraction?.Invoke();
        Debug.Log("ho interagito");
    }
}
