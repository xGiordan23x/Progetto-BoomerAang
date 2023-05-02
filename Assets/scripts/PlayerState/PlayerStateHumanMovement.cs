using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerStateHumanMovement : State, ISubscriber
{
    private Player _player;
    
    public PlayerStateHumanMovement(Player player)
    {
        _player = player;
        PubSub.Instance.RegisteredSubscriber(nameof(PlayerStateHumanMovement), this);
    }
    public override void OnEnter()
    {
        Debug.Log("Sono in human movement");
        _player.GetComponent<SpriteRenderer>().color = Color.green;
       
    }

    public void OnNotify(object content)
    {
        if(content is PotionGenerator)
        {
            _player.isReturning= true;
        }
       
    }

    public override void OnUpdate()
    {
        //Movement

        _player.Move();

        //interaction

        if (Input.GetButtonDown("Use"))
        {
            _player.Interaction();
        }



        if (_player.isReturning)
        {
            _player.stateMachine.SetState(PlayerStateType.BoomerangReturning);
        }


    }

}
