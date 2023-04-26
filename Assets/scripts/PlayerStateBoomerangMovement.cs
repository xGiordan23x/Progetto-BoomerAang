using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerStateBoomerangMovement : State
{
    private Player _player;
    private float elapsedTime;


    public PlayerStateBoomerangMovement(Player player)
    {
        _player = player;
    }

    public override void OnEnter()
    {
        Debug.Log("Sono in Boomerang");


        //imposta il punto uno sotto il player
        _player.curve[0].transform.position = _player.transform.position;
        //imposto l'altro punto sotto il generatore
        _player.curve[1].transform.position = _player.potionGenerator.position;

        _player.SetCurveHandles(_player.lastDirection);
        elapsedTime = 0;
        
    }
    public override void OnUpdate() 
    {
        //Ritono a base parabola
        elapsedTime += Time.deltaTime;
        float percentage = elapsedTime / _player.returnTimer;
        
        _player.transform.position = _player.curve.GetPointAt(percentage);

       if(percentage >= 1)
        {
           _player.isReturning= false;
           _player.stateMachine.SetState(PlayerStateType.HumanMovement);
            
        }
        
    }

}
