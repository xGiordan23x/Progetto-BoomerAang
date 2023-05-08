using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scatola : Interactable
{
    public LayerMask mask;

    Vector3 startPosition;
    Vector3 endPosition;

    private bool isMoving = false;

    private float timeToMove = 0.5f;


    public override void Interact(Player player)
    {
        if (player.stateMachine.GetCurrentState() is PlayerStateHumanMovement)
        {
            if (!isMoving)
            {
                StartCoroutine(MoveBox(player.lastDirection));

                base.Interact(player);          //per effetti visivi/sonori
            }
        }

       


    }

    private void Update()
    {

    }

    private IEnumerator MoveBox(Vector3 direction)
    {
        List<RaycastHit2D> hit2Ds = new List<RaycastHit2D>();

        isMoving = true;

        float elapsedTime = 0;

        startPosition = transform.position;
        endPosition = startPosition + direction;

        if (Physics2D.Raycast(startPosition + (direction * 0.6f), direction, 0.5f, mask))   //brutto da rivedere
        {
            isMoving = false;

            Debug.Log("non si va");
            yield break;
        }

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
