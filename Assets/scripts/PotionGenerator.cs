using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PotionGenerator : MonoBehaviour
{  
    public float timerBoomerang;
    private float timer;
    private bool started;
    private Player player;
    public bool activated;

    private void Start()
    {
        started = false;
        timer = 0;
        activated = true;
    }
    internal void StartTimer(Player playerRef )
    {
        Debug.Log("Timer avviato");
        timer = timerBoomerang;
        started = true;
        player= playerRef;
        player.hasPotion = true;

    }

    private void Update()
    {

       if( Input.GetKeyDown(KeyCode.E))
        {
            activated = true;
        }

        if (activated)
        {

            if (started)
            {
                timer -= Time.deltaTime;
                
                if (timer <= 0)
                {
                    timer = 0;
                    Debug.Log("timer Finito");
                    player.isReturning = true;
                    started = false;
                    activated= false;
                    
                }
            }
        }
    }

}
