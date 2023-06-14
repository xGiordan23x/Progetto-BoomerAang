using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractUI : MonoBehaviour
{
    [SerializeField] GameObject UIInteractBox;

    private void Awake()
    {
        DisabilitateBox();
    }

    public void ActivateBox()
    {
        UIInteractBox.SetActive(true);
    }

    public void DisabilitateBox()
    {
        UIInteractBox.SetActive(false);
    }

    
}
