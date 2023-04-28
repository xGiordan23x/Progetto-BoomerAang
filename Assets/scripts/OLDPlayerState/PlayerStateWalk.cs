using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWalk : State
{
    private Player _player;

    public PlayerStateWalk(Player player)
    {
        _player = player;
    }
}
