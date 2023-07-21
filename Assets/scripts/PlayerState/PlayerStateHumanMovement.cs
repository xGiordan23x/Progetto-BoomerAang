using UnityEngine;

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
        _player.SetCanMove(1);
        
        Debug.Log("Sono in human movement");
        _player.animator.SetBool("BoomerangMoving", false);
        PubSub.Instance.SendMessageSubscriber(nameof(Fontanella), this);
        _player.footCollider.offset = new Vector2(0, _player.yColliderHuman);
        _player.foot.transform.localPosition = new Vector3(0, _player.yPiediHuman);

    }

    public void OnNotify(object content, bool vero = false)
    {
        if (content is PotionGenerator)
        {
            //setto animazione trasformazione con funzione SetIsReturning a true
            _player.animator.SetBool("transform", true);

        }
        if (content is BloccoUmanoParabola)
        {
            //setto animazione trasformazione con funzione SetIsReturning a true
            _player.animator.SetBool("transform", true);

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

            if (Input.GetKeyDown(KeyCode.Space))    
            {
                //torno manualmente a boomerang. ingnorando il timer

                //_player.SetIsReturning();
                _player.animator.SetBool("transform", true);
            }

        }


        if (_player.isReturning)
        {
            PubSub.Instance.SendMessageSubscriber(nameof(PotionGenerator),this);
            _player.stateMachine.SetState(PlayerStateType.BoomerangReturning);

        }



    }


}
