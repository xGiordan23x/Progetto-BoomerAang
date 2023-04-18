using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Curve")]
    public BezierCurve curve;
    public BezierPoint[] points;
    public Vector2 lastDirection;
    public float speed;
    public bool isReturning;

   
    private void Update()
    {
        if (!isReturning)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(horizontal, vertical, 0);
            if(movement != Vector3.zero) 
            {
                Move(movement);
            }
        }
        else
        {
            curve[1].transform.position = transform.position;
           
        }
        
    }

    public void Move(Vector3 movement)
    {
        transform.Translate(movement.normalized * speed * Time.deltaTime);
        lastDirection = movement;
    }
}
