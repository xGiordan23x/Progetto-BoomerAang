using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHumanMovement : State
{
    private Player _player;

    public PlayerStateHumanMovement(Player player)
    {
        _player = player;
    }
    public override void OnEnter()
    {
        Debug.Log("Sono in Walk");
    }
    public override void OnUpdate()
    {
        if (_player.isReturning)
        {
            _player.stateMachine.SetState(PlayerStateType.BoomerangMovement);
        }


    }

}
