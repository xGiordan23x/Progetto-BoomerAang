using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerStateBoomerangMovement : State
{
    private Player _player;

    public PlayerStateBoomerangMovement(Player player)
    {
        _player = player;
    }

    public override void OnEnter()
    {
        Debug.Log("Sono in Boomerang");


        //imposta il punto uno sotto il player
        _player.curve[1].transform.position = _player.transform.position;
        _player.SetCurveHandles(_player.lastDirection);
        
    }
    public override void OnUpdate() 
    {
        //Ritono a base parabola
    }

}
