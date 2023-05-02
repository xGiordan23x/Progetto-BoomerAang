using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerStateBoomerangMovement : State, ISubscriber
{
    private Player _player;
    
   
    public PlayerStateBoomerangMovement(Player player)
    {
        _player = player;
        PubSub.Instance.RegisteredSubscriber(nameof(PlayerStateBoomerangMovement), this);
    }

    public override void OnEnter()
    {
        Debug.Log("Sono in Boomerang movimento"); 
        _player.GetComponent<SpriteRenderer>().color = Color.white;
        _player.hasPotion = false;
        PubSub.Instance.SendMessageSubscriber(nameof(CurveModifier),this);
    }

    public void OnNotify(object content)
    {
       
    }

    public override void OnUpdate()
    {
        _player.Move();
        if(_player.hasPotion)
        {
            _player.stateMachine.SetState(PlayerStateType.HumanMovement);
        }
    }


    }
