﻿using UnityEngine;
using System.Collections;

public class BaseFloorGenerator : MonoBehaviour {

    //Attributes

    public GameObject platform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;  

	// Use this for initialization
	void Start () {
        platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
    }
	
	// Update is called once per frame
	void Update () {
        
        if(transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + platformWidth, transform.position.y, transform.position.z);

            Instantiate(platform, transform.position, transform.rotation); 
        }
	}
}
