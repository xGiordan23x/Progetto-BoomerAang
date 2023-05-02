using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerStateHumanMovement : State
{
    private Player _player;

    public PlayerStateHumanMovement(Player player)
    {
        _player = player;
    }
    public override void OnEnter()
    {
        Debug.Log("Sono in human movement");
       
    }
    public override void OnUpdate()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        if (movement != Vector2.zero)//controllo cosi che lastDirection non sia 0,0
        {

            _player.ChangeLastDirection(movement);

            //prova
            _player.ChangeInteractionVerse();
        }


        _player.rb.velocity = new Vector2(movement.x * _player.humanSpeed, movement.y * _player.humanSpeed);

        //_player.FlipSprite(horizontal);         //flippo lo sprite in X

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
