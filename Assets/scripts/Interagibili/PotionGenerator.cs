using UnityEngine;

public class PotionGenerator : Interactable, ISubscriber
{
    public int timerToAddFromFountain;
    public float timerBoomerang;

    private float timer;
    private bool started;
    [SerializeField] bool isActive;
    [SerializeField] bool canStartOperateWithChip;


    private void Start()
    {
        started = false;
        timer = 0;

        PubSub.Instance.RegisteredSubscriber(nameof(PotionGenerator), this);

    }


    internal void StartTimer()
    {
        Debug.Log("Timer avviato");
        timer = timerBoomerang;

        PubSub.Instance.SendMessageSubscriber(nameof(Player), this);
        started = true;
    }

    private void Update()
    {

        if (started)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = 0;
                Debug.Log("timer Finito");
                started = false;

                PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateHumanMovement), this);

            }
        }

    }

    public void OnNotify(object content)
    {
        if (content is Fontanella)  // mettere fontanella
        {
            timer += timerToAddFromFountain;
        }
        if (content is Player)
        {
            StartTimer();
        }
    }
    public override void Interact(Player player)
    {

        if (player.stateMachine.GetCurrentState() is PlayerStateBoomerangMovement && isActive)   //il generatore puo essere usato solo dal boomerang che cammino
        {
            //avvio animazone bevi pozione con event che chiama isDrinkingPotion
            player.animator.SetBool("DrinkPotion", true);

            player.potionGenerator = transform;   //setto questo generatore come punto di ritorno;
            base.Interact(player);
            StartTimer();
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
}
