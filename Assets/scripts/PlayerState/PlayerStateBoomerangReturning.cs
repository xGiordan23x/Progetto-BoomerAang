using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBoomerangReturning : State
{
    private Player _player;
    private float elapsedTime;


    public PlayerStateBoomerangReturning(Player player)
    {
        _player = player;
    }

    public override void OnEnter()
    {
        Debug.Log("Sono in Boomerang ritorno");
       

        //imposta il punto uno sotto il player
        _player.points[0].transform.position = _player.transform.position;
        //imposto l'altro punto sotto il generatore
        _player.points[1].transform.position = _player.potionGenerator.position;

        _player.SetCurveHandles(_player.lastDirection);
        elapsedTime = 0;

    }
    public override void OnUpdate()
    {
        //Ritono a base parabola
        elapsedTime += Time.deltaTime;
        
        float distanceTimer = _player.curve.length  *  _player.returnTimer/10;
        float percentage = elapsedTime /distanceTimer;

        _player.transform.position = _player.curve.GetPointAt(percentage);

        if (percentage >= 1)
        {
            _player.isReturning = false;
            _player.stateMachine.SetState(PlayerStateType.BoomerangMovement);

        }

    }


}
