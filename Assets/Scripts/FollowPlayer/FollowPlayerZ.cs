﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerZ : MonoBehaviour {

    public GameObject player;
    public float atDistance = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(transform.position.x,
                                              transform.position.y,
                                              player.transform.position.z + atDistance);
	}
}
