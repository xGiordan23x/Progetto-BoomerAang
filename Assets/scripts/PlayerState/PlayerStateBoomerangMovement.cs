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
    }

    public void OnNotify(object content)
    {
       
    }

    public override void OnUpdate()
    {
        //movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        if (movement != Vector2.zero)//controllo cosi che lastDirection non sia 0,0
        {
            _player.ChangeLastDirection(movement);
            _player.ChangeInteractionVerse();
        }

        _player.rb.velocity = new Vector2(movement.x * _player.humanSpeed, movement.y * _player.humanSpeed);

        //_player.FlipSprite(horizontal);  //flippo lo sprite per X


        //interaction

        if (Input.GetButtonDown("Use"))
        {
            _player.Interaction();
        }

        if (_player.hasPotion)
        {
            _player.stateMachine.SetState(PlayerStateType.HumanMovement);
        }
    }


    }
