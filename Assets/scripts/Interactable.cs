using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact(Player player)
    {
        Debug.Log("ho interagito");
    }
}
