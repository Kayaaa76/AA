﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    //Vector3 communityMap;
    //float communityOrth;

    //Vector3 hospitalMap;
    //float hospitalOrth;

    public GameObject minimapBtn;
    public GameObject minimap;

    // Start is called before the first frame update
    void Start()
    {
        //communityMap = new Vector3(9.6f, -63.0f, -40.0f);
        //communityOrth = 33f;

        //hospitalMap = new Vector3(2.9f, 1.4f, -40.8f);
        //hospitalOrth = 12f;

        //gameObject.transform.position = hospitalMap;
        //gameObject.GetComponent<Camera>().orthographicSize = hospitalOrth;
        //Debug.Log(gameObject.transform.position + " " + gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void maximiseMinimap()
    {
        minimapBtn.SetActive(false);
        minimap.SetActive(true);
    }

    public void minimiseMinimap()
    {
        minimapBtn.SetActive(true);
        minimap.SetActive(false);
    }
}
