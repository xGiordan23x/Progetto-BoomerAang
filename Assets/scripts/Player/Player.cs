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
    private float speed;
    public Rigidbody2D Rb;
    [Header("VariabiliBoomerang")]
    public float returnTimer;
    public Transform potionGenerator;
    public CircleCollider2D boomerangCollider;
    public float boomerangReturningRange;


    public bool Interact;
    public bool hasPotion;


    private SpriteRenderer spriteRenderer;

    [Header("VariabiliInteractiblePoint")]
    public Interacter interactionPoint;
    [SerializeField] float MoltiplicationDistance = 0.5f;


    [HideInInspector] public bool isReturning;

    [HideInInspector] public bool canMove;


    public StateMachine<PlayerStateType> stateMachine = new();

    private void Start()
    {
        animator = GetComponent<Animator>();
        PubSub.Instance.RegisteredSubscriber(nameof(Player), this);
        points = curve.GetAnchorPoints();
        Rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stateMachine.RegisterState(PlayerStateType.HumanMovement, new PlayerStateHumanMovement(this));
        stateMachine.RegisterState(PlayerStateType.BoomerangMovement, new PlayerStateBoomerangMovement(this));
        stateMachine.RegisterState(PlayerStateType.BoomerangReturning, new PlayerStateBoomerangReturning(this));

        stateMachine.SetState(PlayerStateType.BoomerangMovement);
        speed = humanSpeed;

    }
    private void Update()
    {

        stateMachine.Update();
        animator.SetFloat("X", lastDirection.x);
        animator.SetFloat("Y", lastDirection.y);
        animator.SetFloat("speed", Rb.velocity.magnitude);

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

        if (collision.GetComponent<Interactable>() != null && stateMachine.GetCurrentState() is PlayerStateBoomerangReturning)
        {
            collision.GetComponent<Interactable>().Interact(this);
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
        if (content is CurveModifier)
        {
            PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateBoomerangReturning), this);
        }


        if (content is BloccoUmanoBoomerang)
        {
            SetCanMove(0);
            animator.SetTrigger("humanboomerang");

        }      
        if (content is BloccoStop)
        {
            SetCanMove(0);
            animator.SetTrigger("Bloccato"); 
        }


        if (content is Fontanella)
        {
            animator.SetTrigger("InteractFountain");
        }
        if (content is BloccoUmanoParabola)
        {
            SetCanMove(0);
        }

    }


    //Da mettere in GameManager
    public LineRenderer lineaProva;

    internal void DrawCurve()
    {
        lineaProva.positionCount = (int)curve.length;
        for (int i = 0; i < curve.length - 1; i++)
        {
            float percentage = Mathf.InverseLerp(0, curve.length - 1, i);

            lineaProva.SetPosition(i, curve.GetPointAt(percentage));

        }
    }



    //prove per interazione

    public void ChangeInteractionVerse()
    {
        Transform verse = interactionPoint.GetComponent<Transform>();

        verse.localPosition = lastDirection * MoltiplicationDistance;
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

        Rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
    }



    public void DestroyBoomerangCollider()
    {
        if (boomerangCollider != null)
        {
            Destroy(boomerangCollider);
        }
    }

    public void AddBomerangCollider()
    {
        if (boomerangCollider == null)
        {
            boomerangCollider = gameObject.AddComponent<CircleCollider2D>();

            boomerangCollider.radius = boomerangReturningRange;
            boomerangCollider.isTrigger = true;
        }

    }

    public void SetIsReturning()
    {
        isReturning = true;
        animator.SetBool("transform", false);

    }
    public void SetCanMove(int i)
    {
        if (i == 0)
        {
            canMove = false;
            speed = 0;


        }
        else
        {
            canMove = true;
            speed = humanSpeed;

        }

    }
    public void isDrinkingPotion()
    {
        PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator), this);
        animator.SetBool("DrinkPotion", false);
    }

    public void nomeDaDare()
    {
       animator.SetBool("BoomerangMoving", true);
    }

}
