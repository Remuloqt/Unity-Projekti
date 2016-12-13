using System.Collections;
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

        GameObject collidersObject = collider.transform.parent.gameObject;
        GameObject carObject = collidersObject.transform.parent.gameObject;

        Vector3 tempVelocity = carObject.GetComponent<Rigidbody>().velocity;

        carObject.transform.position = teleportationDestination.transform.position;
        carObject.transform.rotation = teleportationDestination.transform.rotation;
        carObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, direction * tempVelocity.z);
    }
}
