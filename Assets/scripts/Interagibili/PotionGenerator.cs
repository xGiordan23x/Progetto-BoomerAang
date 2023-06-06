using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PotionGenerator : Interactable, ISubscriber
{
   
    public float timerBoomerang;

    private float timer;
    private bool started;
    public bool stopTimer;
    [SerializeField] bool isActive;
    [SerializeField] bool canStartOperateWithChip;
    [SerializeField] TextMeshProUGUI timerTextValue;

    private void Start()
    {
        started = false;
        timer = 0;
        UpdateTimerText();

        PubSub.Instance.RegisteredSubscriber(nameof(PotionGenerator), this);

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



    public void OnNotify(object content,bool vero = false)
    {
       if(content is PlayerStateHumanMovement)
        {
            ResetTimer();
        }
        if (content is Player)
        {
            StartTimer();
        }
        if (content is BloccoUmanoBoomerang)
        {
            StopTimer();
        }
        if(content is BloccoUmanoParabola)
        {
            StopTimer();
           
        }
    }
    public void StartTimer()
    {
        Debug.Log("Timer avviato");
        timer = timerBoomerang;
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
            started = false;


            PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateHumanMovement), this);
        }
    }



    private void StopTimer()
    {
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
            
        }

        if (player.stateMachine.GetCurrentState() is not PlayerStateBoomerangReturning && !isActive)  //diversamente puo essere attivato con un chip da tutte le forme tranne quando è un boomerang che torna indietro
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
                }

            }


        }
    }

    public void AbilitateGenerator()
    {
        isActive = true;
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

}
