using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scatola : Interactable
{
    public LayerMask mask;

    Vector3 startPosition;
    Vector3 endPosition;

    

    private bool isMoving = false;

    private float timeToMove=0.5f;


    public override void Interact(Player player)
    {
        if (!isMoving)
        {
            

        }


    }

    private void Update()
    {

    }

    private IEnumerator MoveBox(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        startPosition = transform.position;

        isMoving = false;
    }
}
