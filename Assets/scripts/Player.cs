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


    private void Start()
    {
        points = curve.GetAnchorPoints();
        Debug.Log("agagaag");
    }
    private void Update()
    {
        if (!isReturning)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(horizontal, vertical, 0);
            if (movement != Vector3.zero)
            {
                Move(movement);
            }
        }
        else
        {
            curve[1].transform.position = transform.position;
            SetCurveHandles(lastDirection);

        }

    }

    private void SetCurveHandles(Vector2 lastDirection)
    {

        foreach(BezierPoint p in points)
        {
            p.handle1 = Vector3.zero;
            p.handle2 = Vector3.zero;
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
                p.handle1 += new Vector3(-LeftCurveDistance,0);
            }
            else if (lastDirection == Vector2.right)
            {
                p.handle1 += new Vector3(RightCurveDistance,0);
            }
        }
        
    }

    

    public void Move(Vector3 movement)
    {
        transform.Translate(movement.normalized * speed * Time.deltaTime);
        lastDirection = movement;
    }

}
