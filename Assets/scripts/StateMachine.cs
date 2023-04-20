
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerStateType 
{ 
    Idle,
    Walk,
    Boomerang
}

public  class StateMachine<T> where T : Enum
{
    private Dictionary<T, State> _states = new();
    private State _currentState;

    public void RegisterState(T type, State state)
    {
        if( ( _states.ContainsKey(type)))
        {
            throw new Exception("Stato gia presente " + type);
        }

        _states.Add(type, state);   
    }

    public void SetState( T type) 
    {
        if ((_states.ContainsKey(type)))
        {
            throw new Exception("Stato gia presente " + type);
        }

        _currentState?.OnExit();
        _currentState = _states[type];
        _currentState.OnEnter();
    }

    public void Update()
    {
        _currentState?.OnUpdate();
    }
}
