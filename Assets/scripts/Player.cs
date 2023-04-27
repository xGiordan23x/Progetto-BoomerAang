using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    [Header("Curva")]
    public BezierCurve curve;
    private BezierPoint[] points;
    public Vector2 lastDirection;
    [Header("LarghezzaCurvaPerDirezione")]
    public AnimationCurve animationCurve;
    public float curveDistance;
    [Header("VariabiliUmano")]
    public Animator animator;
    public float humanSpeed;
    public bool isReturning;
    [HideInInspector]public Rigidbody2D rb;
    [Header("VariabiliBoomerang")]   
    public float returnTimer;
    public Transform potionGenerator;
    

    public StateMachine<PlayerStateType> stateMachine = new();

    private void Start()
    {
        points = curve.GetAnchorPoints();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.RegisterState(PlayerStateType.HumanMovement, new PlayerStateHumanMovement(this));
        stateMachine.RegisterState(PlayerStateType.BoomerangMovement, new PlayerStateBoomerangMovement(this));

        stateMachine.SetState(PlayerStateType.HumanMovement);
    }
    private void Update()
    {

        stateMachine.Update();
    }

   
    public void SetCurveHandles(Vector2 lastDirection)
    {
        Vector3 GeneratorDirection = potionGenerator.position - transform.position;
        float temp = Vector3.Dot(lastDirection, GeneratorDirection);
        temp = (temp + 1) / 2f;

        //float DistanzaHandle = animationCurve.Evaluate(temp) * curveDistance;

        points[0].handle2 = lastDirection * curveDistance; // DistanceHandle
        points[1].handle1 = lastDirection * curveDistance; // DistanceHandle

    }

}
