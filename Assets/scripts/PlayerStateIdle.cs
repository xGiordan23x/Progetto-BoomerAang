using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : State
{
    private Player _player;

    public PlayerStateIdle(Player player)
    {
        _player= player;
    }
}
