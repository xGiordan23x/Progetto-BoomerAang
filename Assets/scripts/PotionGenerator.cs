using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PotionGenerator :Interactable ,ISubscriber
{  
    public float timerBoomerang;
    public float timer;
    private bool started;
    private Player player;
    
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
        started = true;
        PubSub.Instance.SendMessageSubscriber(nameof(Player),this);
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

                    PubSub.Instance.SendMessageSubscriber(nameof(PlayerStateHumanMovement),this);
                   
                }
            }
        
    }

    public void OnNotify(object content)
    {
       if (content is Player)
        {
            StartTimer();
        }
    }
    public override void Interact(Player player)
    {
        base.Interact(player);
        StartTimer();
    }

}
