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
        


    }

    private void Update()
    {

    }

    private IEnumerator MoveBox(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        startPosition = transform.position;
        endPosition = startPosition + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;

        isMoving = false;
    }
}
