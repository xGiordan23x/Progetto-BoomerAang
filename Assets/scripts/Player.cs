using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Curve")]
    public BezierCurve curve;
    private BezierPoint[] points;
    public Vector2 lastDirection;
    [Header("Variabili")]
    public Animator animator;
    public float speed;
    public bool isReturning;
    public Rigidbody2D rigidbody;
    public Vector3 posizioneGeneratorePozione;
    [Header("LarghezzaCurvaPerDirezione")]
    public AnimationCurve animationCurve;
    public float curveDistance;
   

    public StateMachine<PlayerStateType> stateMachine = new();

    private void Start()
    {
        points = curve.GetAnchorPoints();
        rigidbody = GetComponent<Rigidbody2D>();
        stateMachine.RegisterState(PlayerStateType.HumanMovement, new PlayerStateHumanMovement(this));
        stateMachine.RegisterState(PlayerStateType.BoomerangMovement, new PlayerStateBoomerangMovement(this));

        //stateMachine.SetState(PlayerStateType.Idle);
    }
    private void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0);
        if (movement != Vector3.zero)
        {
            Move(movement);
        }
        else if (isReturning)
        {
            curve[1].transform.position = transform.position;
            SetCurveHandles(lastDirection);
        }


    }

    private void MoveAlongCurve()
    {
        //curve.GetPointAt()
    }

    public void SetCurveHandles(Vector2 lastDirection)
    {
        Vector3 GeneratorDirection = posizioneGeneratorePozione - transform.position;
        float temp = Vector3.Dot(lastDirection, GeneratorDirection);
        temp = (temp + 1) / 2f;//controllare
        float DistanzaHandle = animationCurve.Evaluate(temp) * curveDistance;

        points[0].handle2 = lastDirection*DistanzaHandle;
        points[1].handle1 = lastDirection * DistanzaHandle;

    }



    public void Move(Vector3 movement)
    {

        transform.Translate(movement.normalized * speed * Time.deltaTime);
        lastDirection = movement;
    }

}
