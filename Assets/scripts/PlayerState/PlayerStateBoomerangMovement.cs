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

        _player.footCollider.offset = new Vector2(0, _player.yColliderBoomerang);
        _player.foot.transform.localPosition = new Vector3(0, _player.yPiediBoomerang);
        PubSub.Instance.SendMessageSubscriber(nameof(Booster), this);
        PubSub.Instance.SendMessageSubscriber(nameof(Fontanella), this);
        PubSub.Instance.SendMessageSubscriber(nameof(DialogueManager), this);

        //_player.canMove = true;

    }

    public void OnNotify(object content, bool vero = false)
    {
        if(content is Player)
        {
            PubSub.Instance.SendMessageSubscriber(nameof(DialogueManager), this);
        }
        if (content is DialogueManager && vero == true)
        {
            _player.canMove = false;
        }
        if (content is DialogueManager && vero == false)
        {
            _player.SetCanMove(1);
            _player.canMove = true;
        }

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
