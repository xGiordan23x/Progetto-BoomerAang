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
        Debug.Log("Creo un collider");
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

    public void OnNotify(object content)
    {
        if (content is CurveModifier)
        {
            SetCurve(_player.lastDirection);
            stopPlayer = true;
        }
        if(content is Player)
        {
            stopPlayer= false;
        }
    
      if(content is BloccoStop)
        {
            StopParabola();

        }
        if(content is BloccoUmanoBoomerang)
        {
            StopParabola();
        }

       
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
            if (percentage > 0 || percentage < 1) 
            _player.transform.position = _player.curve.GetPointAt(percentage);

            
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
        _player.DestroyBoomerangCollider();
        _player.animator.SetBool("transform", false);


    }






}
