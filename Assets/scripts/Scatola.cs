using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scatola : Interactable
{
    public LayerMask mask;

    Vector3 startPosition;
    Vector3 endPosition;

    [SerializeField] AnimationCurve curve;

    Rigidbody2D rb;

    bool isMoving = false;

    float time;


    public override void Interact(Player player)
    {
        if (!isMoving)
        {
            startPosition = transform.position.normalized;

            endPosition = (player.transform.position.normalized - startPosition);

            Debug.Log(endPosition);

            isMoving = true;

            time = 0;

            base.Interact(player);

        }


    }

    private void Update()
    {
        if (isMoving)
        {
           


        }

    }
}
