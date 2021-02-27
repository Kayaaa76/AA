using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameUIR : MonoBehaviour
{
    public GameObject player; // player game object
    PlayerController playercontroller;

    public Text scoreText;
    public Text enemiesText;
    public Text timeText;
    
    // Start is called before the first frame update
    void Start()
    {
        playercontroller = player.GetComponent<PlayerController>(); // get the player controller component
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = playercontroller.yourScore.ToString();
        enemiesText.text = playercontroller.enemyCount.ToString();
        timeText.text = 60 - Mathf.FloorToInt(playercontroller.timer) - 1 + " seconds";
    }
}
