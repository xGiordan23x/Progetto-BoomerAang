using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Curve")]
    public BezierCurve curve;
    public BezierPoint[] points;
    public Vector2 lastDirection;
    public float speed;
    public bool isReturning;
    public float UpCurveDistance;
    public float DownCurveDistance;
    public float LeftCurveDistance;
    public float RightCurveDistance;
    private bool isFacingRight;
    private bool isFacingLeft;
    private bool isFacingUp;
    private bool isFacingDown;

    Rigidbody2D body;

    public StateMachine<PlayerStateType> stateMachine = new();

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        points = curve.GetAnchorPoints();
        stateMachine.RegisterState(PlayerStateType.Idle, new PlayerStateIdle(this));
        stateMachine.RegisterState(PlayerStateType.Walk, new PlayerStateWalk(this));
        stateMachine.RegisterState(PlayerStateType.Boomerang, new PlayerStateBoomerang(this));
    }
    private void Update()
    {
        if (!isReturning)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(horizontal, vertical).normalized;

            Move(movement);
        }
        else
        {
            //imposta il punto uno sotto il player
            curve[1].transform.position = transform.position;
            SetCurveHandles(lastDirection);
            MoveAlongCurve();

        }

    }

    private void MoveAlongCurve()
    {
      //curve.GetPointAt()
    }

    private void SetCurveHandles(Vector2 lastDirection)
    {
        int temp = 0;
        foreach(BezierPoint p in points)
        {

            p.handle1 = Vector3.zero;
            p.handle2 = Vector3.zero;

           
            if(temp == 1)
            {
                if (lastDirection == Vector2.up)
                {
                    p.handle1 += new Vector3(0, UpCurveDistance);
                   
                }
                else if (lastDirection == Vector2.down)
                {
                    p.handle1 += new Vector3(0, -DownCurveDistance);
                    
                }
                else if (lastDirection == Vector2.left)
                {
                    p.handle1 += new Vector3(-LeftCurveDistance, 0);
                    
                }
                else if (lastDirection == Vector2.right)
                {
                    p.handle1 += new Vector3(RightCurveDistance, 0);
                    
                }
            }

            if (temp == 0)
            {
                if (lastDirection == Vector2.up)
                {
                    p.handle1 += new Vector3(0, -UpCurveDistance);
                    temp++;
                }
                else if (lastDirection == Vector2.down)
                {
                    p.handle1 += new Vector3(0, DownCurveDistance);
                    temp++;
                }
                else if (lastDirection == Vector2.left)
                {
                    p.handle1 += new Vector3(LeftCurveDistance, 0);
                    temp++;
                }
                else if (lastDirection == Vector2.right)
                {
                    p.handle1 += new Vector3(-RightCurveDistance, 0);
                    temp++;
                }
            }
            
        }
        
    }

    

    public void Move(Vector3 movement)
    {
        //transform.Translate(movement.normalized * speed * Time.deltaTime);

        body.MovePosition(transform.position + movement * speed * Time.deltaTime);
        lastDirection = movement;
    }

}
