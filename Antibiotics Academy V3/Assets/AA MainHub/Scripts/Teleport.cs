using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject A; // position to teleport to

    GameObject miniCommunity;

    void Start()
    {
        miniCommunity = GameObject.Find("MiniCommunity");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if player collide with the game object this script is attached to, the player would get teleported to game object A's position
        collision.gameObject.transform.position = A.transform.position;

        if(gameObject.name == "Hospital") //when player leaves community (enters hospital)
        {
            GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false; //disable camera controller script

            miniCommunity.SetActive(false); //disable minimap
        }
        else if(gameObject.name == "Floor_Mat") //when player enters community (leaves hospital)
        {
            GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true; //enable camera controller script

            miniCommunity.SetActive(true); //enable minimap

            collision.gameObject.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, -38f); //set player z value to originally set value(manually in inspector)
        }
    }
}
