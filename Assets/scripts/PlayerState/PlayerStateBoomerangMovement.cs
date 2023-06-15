using UnityEditor.Hardware;
using UnityEngine;

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
        
        _player.isReturning = false;
        _player.hasPotion = false;
        _player.canMove = true;
        _player.footCollider.offset = new Vector2(0, _player.yColliderBoomerang);
        PubSub.Instance.SendMessageSubscriber(nameof(Booster), this);
        PubSub.Instance.SendMessageSubscriber(nameof(Fontanella), this);

    }

    public void OnNotify(object content, bool vero = false)
    {

    }

    public override void OnUpdate()
    {
        if (_player.canMove)
        {
            //Movement
            _player.Move();

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
}
