﻿using UnityEngine;
using System.Collections;
using EM;

public class Main : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
        Materials.initMaterials();
        Sky sky = new Sky();
        for(int x = -4; x < 4; x++)
        {
            for(int z = -4; z < 4; z++)
            {
                sky.addIsland(x, z);
            }
        }
        sky.getIsland(0, 0).setClod(Clod.Stone, 8, 7, 6);
        Debug.Log(Clod.Stone.isSolid);
        Debug.Log(sky.getClod(2,3,4).isSolid);
        Debug.Log(sky.getClod(5, 3, 4).isSolid);
        //搞两个Island
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKey(KeyCode.A))
            this.gameObject.transform.Translate(Vector3.left);
        if (Input.GetKey(KeyCode.D))
            this.gameObject.transform.Translate(Vector3.right);
        if (Input.GetKey(KeyCode.W))
            this.gameObject.transform.Translate(Vector3.forward);
        if (Input.GetKey(KeyCode.S))
            this.gameObject.transform.Translate(Vector3.back);
        if (Input.GetKey(KeyCode.LeftArrow))
            this.gameObject.transform.Rotate(Vector3.down);
        if (Input.GetKey(KeyCode.RightArrow))
            this.gameObject.transform.Rotate(Vector3.up);
    }
}

