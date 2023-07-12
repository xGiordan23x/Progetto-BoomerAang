using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateBoomerangReturning : State, ISubscriber
{
    private Player _player;
    private float elapsedTime;
    float percentage;
    float distanceTimer;

    private bool stopPlayer;

    public PlayerStateBoomerangReturning(Player player)
    {
        _player = player;
        PubSub.Instance.RegisteredSubscriber(nameof(PlayerStateBoomerangReturning), this);
    }

    public override void OnEnter()
    {
        Debug.Log("Sono in Boomerang ritorno");

        _player.GetComponentInChildren<DinamicOrdering>().enabled = false;
        _player.GetComponent<SpriteRenderer>().sortingLayerName = "Tetto";
        _player.GetComponent<SpriteRenderer>().sortingOrder = 100;

        _player.AddBomerangCollider();


        SetCurve(_player.lastDirection);


    }

    private void SetCurve(Vector2 direction)
    {
        //imposta il punto uno sotto il player
        _player.points[0].transform.position = _player.transform.position;
        //imposto l'altro punto sotto il generatore
        _player.points[1].transform.position = _player.potionGenerator.position;

        _player.SetCurveHandles(direction);
        elapsedTime = 0;
        _player.DrawCurve();
    }
    private void SetLinearCurve()
    {
        //imposta il punto uno sotto il player
        _player.points[0].transform.position = _player.transform.position;
        //imposto l'altro punto sotto il punto di arrivo
        _player.points[1].transform.position = _player.arrivePoint.position;

        _player.points[0].handle1 = new Vector3(0,0, 0);     
        _player.points[1].handle1 = new Vector3(0,0, 0);
    }

    public void OnNotify(object content, bool vero = false)
    {
        if (content is Booster)
        {
            SetCurve(_player.lastDirection);
            stopPlayer = true;
        }
        if(content is Player)
        {
            SetLinearCurve();
            stopPlayer= false;
        }
    
      if(content is BloccoStop)
        {
            StopParabolaBloccoStop();

        }
        if(content is BloccoUmanoBoomerang)
        {
            StopParabola();
        }

       
    }

    private void StopParabolaBloccoStop()
    {
        _player.isReturning = false;
       
        _player.stateMachine.SetState(PlayerStateType.BoomerangMovement);
    }

    public override void OnUpdate()
    {

        if (!stopPlayer)
        {
            //Ritono a base parabola
            elapsedTime += Time.deltaTime;

          distanceTimer = _player.curve.length * _player.returnTimer / 10;
          percentage = elapsedTime / distanceTimer;

            if (percentage >= 1)
            {
                StopParabola();

            }

            if (percentage > 0 && percentage < 1)
            {
                _player.transform.position = _player.curve.GetPointAt(percentage);
            }
          

            
        }



    }

    private void StopParabola()
    {
        _player.isReturning = false;
        _player.animator.SetBool("BoomerangMoving", true);
        _player.stateMachine.SetState(PlayerStateType.BoomerangMovement);
    }

    public override void OnExit()
    {
        _player.GetComponentInChildren<DinamicOrdering>().enabled = true;
        _player.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        _player.GetComponent<SpriteRenderer>().sortingOrder = 0;


        _player.DestroyBoomerangCollider();
        _player.animator.SetBool("transform", false);


    }






}
