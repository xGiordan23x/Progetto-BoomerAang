using System;
using UnityEngine;

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


    private SpriteRenderer spriteRenderer;

    public Interacter interactionPoint;

    [HideInInspector] public bool isReturning;




    public StateMachine<PlayerStateType> stateMachine = new();

    private void Start()
    {
        PubSub.Instance.RegisteredSubscriber(nameof(Player), this);
        points = curve.GetAnchorPoints();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (generator != null && (stateMachine.GetCurrentState() is PlayerStateBoomerangMovement))
        {
            PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator), this);
        }

        else
        {

        }
    }


    public void FlipSprite(float speed)
    {
        if (transform.localScale.x * speed < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Interaction()
    {
        interactionPoint.Interaction(this);
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


    //prove per interazione

    public void ChangeInteractionVerse()
    {
        Transform verse = interactionPoint.GetComponent<Transform>();

        verse.localPosition = lastDirection;
    }

    public void ChangeLastDirection(Vector2 movement)
    {
        if (movement.y > 0)
        {
            movement.y = 1;
            movement.x = 0;
        }
        if (movement.y < 0)
        {
            movement.y = -1;
            movement.x = 0;
        }

        lastDirection = movement;
    }

}
    