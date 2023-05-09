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


    private SpriteRenderer spriteRenderer;

    public Interacter interactionPoint;

    [HideInInspector] public bool isReturning;




    public StateMachine<PlayerStateType> stateMachine = new();

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        animator.SetFloat("X",lastDirection.x);
        animator.SetFloat("Y",lastDirection.y);
        animator.SetFloat("speed",rb.velocity.magnitude);

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
        
        CurveModifier curveModifier = collision.GetComponent<CurveModifier>();

        if (curveModifier != null && (stateMachine.GetCurrentState() is PlayerStateBoomerangReturning))
        {
            lastDirection = curveModifier.newDirection;
            PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);
            PubSub.Instance.SendMessageSubscriber(nameof(CurveModifier), this);
            curveModifier.activated = true;
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


    public void Move()
    {     
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized;

        if (movement != Vector2.zero)//controllo cosi che lastDirection non sia 0,0
        {
            lastDirection = movement;

           ChangeLastDirection(movement);

            //prova
            ChangeInteractionVerse();
        }
        if(movement == Vector2.zero)
        {
            animator.SetBool("movement", false);
            animator.SetBool("idle", true);

        }
        

        rb.velocity = new Vector2(movement.x *  humanSpeed, movement.y * humanSpeed);
    }

}
    