using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateBoomerangReturning : State, ISubscriber
{
    private Player _player;
    private float elapsedTime;
    


    public PlayerStateBoomerangReturning(Player player)
    {
        _player = player;
        PubSub.Instance.RegisteredSubscriber(nameof(PlayerStateBoomerangReturning), this);
    }

    public override void OnEnter()
    {
        Debug.Log("Sono in Boomerang ritorno");
        _player.GetComponent<SpriteRenderer>().color = Color.yellow;

        Debug.Log("Creo un collider");
        _player.AddBomerangCollider();

        SetCurve(_player.lastDirection); //sta cosa da errori , da risolvere        Fede.

        

        //CircleCollider2D circle = _player.AddComponent<CircleCollider2D>();
        //_player.boomerangCollider = circle;
        //_player.boomerangCollider.radius = _player.boomerangReturningRange;
        
    }

    private void SetCurve(Vector2 direction)
    {
        //imposta il punto uno sotto il player
        _player.points[0].transform.position = _player.transform.position;
        //imposto l'altro punto sotto il generatore
        _player.points[1].transform.position = _player.potionGenerator.position;

        _player.SetCurveHandles(direction);
        elapsedTime = 0;
        _player.DrawCurve();
    }

    public void OnNotify(object content)
    {
        if (content is CurveModifier)
        {
            SetCurve(_player.lastDirection);
        }
    }



    public override void OnUpdate()
    {
        //Ritono a base parabola
        elapsedTime += Time.deltaTime;

        float distanceTimer = _player.curve.length * _player.returnTimer / 10;
        float percentage = elapsedTime / distanceTimer;

        _player.transform.position = _player.curve.GetPointAt(percentage);

        if (percentage >= 1)
        {
            _player.isReturning = false;
            _player.stateMachine.SetState(PlayerStateType.BoomerangMovement);

        }



    }

    public override void OnExit()
    {
        _player.DestroyBoomerangCollider();
    }






}
