using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PotionGenerator : Interactable, ISubscriber
{
   
    public float timerBoomerang;

    private float timer;
    [SerializeField] bool started;
    public bool stopTimer;
    Animator animator;
    [SerializeField] bool isActive;
    [SerializeField] bool canStartOperateWithChip;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI timerTextValue;
    [SerializeField] Image timerImage;
    [SerializeField] Animator timerAnimator;
    [Header("Audio")]
    [SerializeField] AudioClip ClipUtilizzaChip;

    private void Awake()
    {
        timerTextValue = FindObjectOfType<GeneratorTimer>().gameObject.GetComponentInChildren<TextMeshProUGUI>();
        timerAnimator = FindObjectOfType<GeneratorTimer>().gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        started = false;
        timer = 0;
        UpdateTimerText();

        PubSub.Instance.RegisteredSubscriber(nameof(PotionGenerator), this);

        if (isActive)
        {
            animator.SetBool("Activate", true);
        }

    }




    private void Update()
    {
        if (started)
        {
            if (!stopTimer)
            {
                UpdateTimer();
            }


        }
    }



    public void OnNotify(object content, bool vero = false)
    {
        if (content is PlayerStateHumanMovement)
        {
            ResetTimer();
        }
        if (content is Player)
        {
            //StartTimer();
        }
        if (content is BloccoUmanoBoomerang)
        {
            StopTimer();
        }
        if (content is BloccoUmanoParabola)
        {
            StopTimer();

        }
        if (content is DialogueManager && vero)
        {
            stopTimer = true;
        }
        if (content is DialogueManager && !vero)
        {
            stopTimer = false;
        }
        if (content is Leva && vero)
        {
            stopTimer = true;
        }
        if (content is Leva && !vero)
        {
            stopTimer = false;
        }

    }
    public void StartTimer()
    {
        Debug.Log("Timer avviato");
        timer = timerBoomerang;
        timerAnimator.SetBool("TimerOn", true);
        UpdateTimerText();

        PubSub.Instance.SendMessageSubscriber(nameof(Player), this);
        started = true;
        stopTimer = false;

    }
    public void UpdateTimer()
    {

        timer -= Time.deltaTime;

       
        UpdateTimerText();

        if (timer <= 0)
        {
            timer = 0;
            Debug.Log("timer Finito");
            StopTimer();
            started = false;

            PubSub.Instance.SendMessageSubscriber(nameof(DialogueManager), this);
            PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateHumanMovement), this);
        }
    }



    private void StopTimer()
    {
        timerAnimator.SetBool("TimerOn", false);
        timer = 0;
        stopTimer = true;
        started = false;
        UpdateTimerText();

    }

    public override void Interact(Player player)
    {

        if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangMovement && isActive)   //il generatore puo essere usato solo dal boomerang che cammino
        {
            //avvio animazone bevi pozione con event che chiama isDrinkingPotion
            player.animator.SetBool("DrinkPotion", true);

            player.potionGenerator = transform;   //setto questo generatore come punto di ritorno;
            base.Interact(player);
            stopTimer = false;

            //fede solution
            StartTimer();

        }

        if (player.stateMachine.GetCurrentState() is not PlayerStateBoomerangReturning && !isActive)  //diversamente puo essere attivato con un chip da tutte le forme tranne quando � un boomerang che torna indietro
        {
            if (!canStartOperateWithChip)
            {
                Debug.Log("non posso attivarlo con il chip");
                return;
            }

            Inventory inventory = player.GetComponent<Inventory>();

            if (inventory != null)
            {
                if (inventory.UseChip())
                {
                    AbilitateGenerator();
                    //mettere animazione utilizzo chip

                    if (player.stateMachine.GetCurrentState() is PlayerStateHumanMovement)
                    {
                        player.SetCanMove(0);
                        player.animator.SetTrigger("OpenDoor");
                    }

                    if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangMovement)
                    {
                        player.SetCanMove(0);
                        player.animator.SetTrigger("BoomerangInteract");
                    }

                }

            }


        }
    }

    public void AbilitateGenerator()
    {
        isActive = true;
        animator.SetBool("Activate", true);
    }
    public void UpdateTimerText()
    {
      
        timerTextValue.text = Mathf.RoundToInt(timer).ToString();
       
    }
    public void ResetTimer()
    {
        StopTimer();
        UpdateTimerText();
    }

    public void IncreaseTimerFontanella(float timerToAdd)
    {
        stopTimer= true;
        timer += timerToAdd;  
        stopTimer= false;
       
    }

    public void PlayAudioClipUtilizzaChiave()
    {
        AudioManager.instance.PlayAudioClip(ClipUtilizzaChip);
    }


}
