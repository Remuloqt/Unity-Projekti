﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationSphere : MonoBehaviour {

    public float direction = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        GameObject teleportationDestination = transform.parent.Find("TeleportationDestination").gameObject;

        if (collider.transform.parent.gameObject == null) return;

        GameObject collidersObject = collider.transform.parent.gameObject;
        GameObject carObject = collidersObject.transform.parent.gameObject;

        carObject.transform.position = teleportationDestination.transform.position;
        carObject.transform.rotation = teleportationDestination.transform.rotation;
        carObject.GetComponent<Rigidbody>().velocity = teleportationDestination.transform.forward * direction;
    }
}
