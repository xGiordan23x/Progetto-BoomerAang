using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Curve")]
    public BezierCurve curve;
    public BezierPoint[] points;
    public Vector2 lastDirection;
    public float speed;
    public bool isReturning;
    private bool isFacingRight;
    private bool isFacingLeft;
    private bool isFacingUp;
    private bool isFacingDown;


    private void Start()
    {
        points = curve.GetAnchorPoints();
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
            //SetCurveHandles(lastDirection);

        }

    }

    //private void SetCurveHandles(Vector2 lastDirection)
    //{
    //    switch (lastDirection)
    //    {
    //        case Vector2(2, 0):

    //            break;

    //        case Vector2.down:
    //            break;

    //        case Vector2.left:
    //            break;

    //        case Vector2.right:
    //            break;



    //    }
    //}

    public void Move(Vector3 movement)
    {
        transform.Translate(movement.normalized * speed * Time.deltaTime);
        lastDirection = movement;
    }

}
