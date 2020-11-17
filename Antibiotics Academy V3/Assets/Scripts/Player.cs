using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int coins;

    public static void TotalCoins()
    {
        Debug.Log("Player has: " + coins + " coins!");
    }
}
