using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coin;

    public PlayerData(Player player)
    {
        coin = player.coin;
    }
}
