using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour, ISubscriber
{

    [Header("Curva")]
    public BezierCurve curve;
    public BezierPoint[] points;
    public Vector2 lastDirection;
    [Header("LarghezzaCurvaPerDirezione")]
    public AnimationCurve animationCurve;
    public float curveDistance;
    [Header("VariabiliUmano")]
    public Animator animator;
    public float humanSpeed;

    [HideInInspector] public Rigidbody2D rb;
    [Header("VariabiliBoomerang")]
    public float returnTimer;
    public Transform potionGenerator;

    public bool Interact;
    public bool hasPotion;

    [HideInInspector] public bool isReturning;



    public StateMachine<PlayerStateType> stateMachine = new();

    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(Player), this);
        points = curve.GetAnchorPoints();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.RegisterState(PlayerStateType.HumanMovement, new PlayerStateHumanMovement(this));
        stateMachine.RegisterState(PlayerStateType.BoomerangMovement, new PlayerStateBoomerangMovement(this));
        stateMachine.RegisterState(PlayerStateType.BoomerangReturning, new PlayerStateBoomerangReturning(this));

        stateMachine.SetState(PlayerStateType.BoomerangMovement);
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

        //float DistanceHandle = animationCurve.Evaluate(temp) * curveDistance;

        points[0].handle2 = lastDirection * curveDistance; // DistanceHandle
        points[1].handle1 = lastDirection * curveDistance;

        //points[0].handle2 = lastDirection * DistanceHandle;
        //points[1].handle1 = lastDirection * DistanceHandle;



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PotionGenerator generator = collision.GetComponent<PotionGenerator>();
        CurveModifier curveModifier = collision.GetComponent<CurveModifier>();
        if (generator != null && (stateMachine.GetCurrentState() is PlayerStateBoomerangMovement))
        {
            // mettere stay e tasto interazione
            PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator), this);
        }

        else if(curveModifier != null && (stateMachine.GetCurrentState() is PlayerStateBoomerangReturning))
        {
            lastDirection = curveModifier.newDirection;
            PubSub.Instance.SendMessageSubscriber(nameof(CurveModifier), this);
            curveModifier.activated = true;
        }
    }

    public void OnNotify(object content)
    {
        if (content is PotionGenerator)
        {
            hasPotion = true;
        }

    }


    //Da mettere in GameManager
    public LineRenderer lineaProva;
    internal void DrawCurve()
    {
        lineaProva.positionCount = (int)curve.length;
        for (int i = 0; i < curve.length-1; i++)
        {
            float percentage = Mathf.InverseLerp(0, curve.length-1, i);
          
            lineaProva.SetPosition(i,curve.GetPointAt(percentage));
            
        }
    }

    public void Move()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized;

        if (movement != Vector2.zero)//controllo cosi che lastDirection non sia 0,0
        {
            lastDirection = movement;
        }
        
        rb.velocity = new Vector2(movement.x *  humanSpeed, movement.y * humanSpeed);
    }
}
    