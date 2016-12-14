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

        if (collider.transform.parent.gameObject == null) return;

        GameObject collidersObject = collider.transform.parent.gameObject;
        GameObject carObject = collidersObject.transform.parent.gameObject;

        Vector3 tempVelocity = carObject.GetComponent<Rigidbody>().velocity;

        carObject.transform.position = teleportationDestination.transform.position;
        carObject.transform.rotation = teleportationDestination.transform.rotation;

        if (tempVelocity.z < teleportationDestination.transform.forward.z) tempVelocity.z *= -1;

        Vector3 vectorToGoFromDestination = new Vector3(teleportationDestination.transform.forward.x, teleportationDestination.transform.forward.y, teleportationDestination.transform.forward.z + tempVelocity.z);
        carObject.GetComponent<Rigidbody>().velocity = vectorToGoFromDestination;
    }
}
