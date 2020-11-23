using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCoins : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = this.GetComponent<Text>();
    }

    void Update()
    {
        text.text = "Coins: " + Player.coins;
    }
}
