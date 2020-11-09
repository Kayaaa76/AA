using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneManager : MonoBehaviour
{
    public static bool SceneActive = true;
    public GameObject Scene;


    void Update()
    {
        if(SceneActive == true)
        {
            Scene.SetActive(true);
        }
        else if (SceneActive == false)
        {
            Scene.SetActive(false);
        }
    }
}
