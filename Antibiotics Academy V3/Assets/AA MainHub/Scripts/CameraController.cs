using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public BoxCollider2D LockedLeft;
    public BoxCollider2D LockedRight;
    public BoxCollider2D LockedUp;
    public BoxCollider2D LockedDown;

    public GameObject miniCommunity;

    Camera cam;

    float camOrthSize;
    float camHorizontalOrth;
    
    float horizontal;
    float vertical;
    
    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponent<UnityEngine.Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        camOrthSize = cam.orthographicSize = 6; // set the orthographic size of the camera to 6
        camHorizontalOrth = camOrthSize * cam.aspect; //get width of current viewing volume
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal = Mathf.Clamp(player.position.x, LockedLeft.bounds.max.x + camHorizontalOrth, LockedRight.bounds.min.x - camHorizontalOrth); //set horizontal min and max range for camera movement
        //vertical = Mathf.Clamp(player.position.y + 2f, LockedDown.bounds.max.y + camOrthSize, LockedUp.bounds.min.y - camOrthSize); //set vertical min and max range for camera movement
        //transform.position = new Vector3(horizontal, vertical, transform.position.z);

        if (player.position.y < -20)
        {
            horizontal = Mathf.Clamp(player.position.x, LockedLeft.bounds.max.x + camHorizontalOrth, LockedRight.bounds.min.x - camHorizontalOrth); //set horizontal min and max range for camera movement
            vertical = Mathf.Clamp(player.position.y + 2f, LockedDown.bounds.max.y + camOrthSize, LockedUp.bounds.min.y - camOrthSize); //set vertical min and max range for camera movement
            transform.position = new Vector3(horizontal, vertical, transform.position.z); //set camera position within the range

            player.position = new Vector3(player.position.x, player.position.y, -38f); //set player z value to originally set value(manually in inspector)

            miniCommunity.SetActive(true);
        }
        else
        {
            miniCommunity.SetActive(false);
        }
    }

}
