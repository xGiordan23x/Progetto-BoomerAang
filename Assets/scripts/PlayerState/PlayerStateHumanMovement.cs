using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        
    }

    public void OnNotify(object content)
    {
        if(content is PotionGenerator)
        {
            //setto animazione trasformazione con funzione SetIsReturning a true
            _player.animator.SetTrigger("transform");
           
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
