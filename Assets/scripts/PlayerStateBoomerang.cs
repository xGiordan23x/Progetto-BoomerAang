using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBoomerang : State
{
    private Player _player;

    public PlayerStateBoomerang(Player player)
    {
        _player = player;
    }
}
