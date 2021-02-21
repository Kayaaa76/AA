using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCoins : MonoBehaviour
{
    Text text;

    public GameObject coinsDisplay;

    void Start()
    {
        text = this.GetComponent<Text>();

        LayoutRebuilder.ForceRebuildLayoutImmediate(coinsDisplay.GetComponent<RectTransform>()); //rebuild layout to display properly
    }

    void Update()
    {
        text.text = Player.coins.ToString();
    }
}
