using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int coins;
    public int coin;

    void Update()
    {
        coin = coins;
    }

    public static void TotalCoins()
    {
        Debug.Log("Player has: " + coins + " coins!");
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        coins = data.coin;
    }
}
