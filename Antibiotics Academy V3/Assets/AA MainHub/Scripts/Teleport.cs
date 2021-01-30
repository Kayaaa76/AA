using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject A; // position to teleport to

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if player collide with the game object this script is attached to, the player would get teleported to game object A's position
        collision.gameObject.transform.position = A.transform.position;

        if(gameObject.name == "Hospital") //when player leaves community (enters hospital)
        {
            GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false; //disable camera controller script
        }
        else GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true; //enable camera controller script
    }
}
