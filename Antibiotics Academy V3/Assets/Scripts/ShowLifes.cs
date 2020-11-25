using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLifes : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = this.GetComponent<Text>();
    }

    void Update()
    {
        text.text = "Lifes: " + Player.lives;
    }
}
