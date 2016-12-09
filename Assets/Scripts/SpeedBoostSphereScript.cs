using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostSphereScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {

        GameObject collidersObject = collider.transform.parent.gameObject;
        GameObject carObject = collidersObject.transform.parent.gameObject;

        Vector3 tempVelocity = carObject.GetComponent<Rigidbody>().velocity;
        tempVelocity.z *= 1.7f;
        carObject.GetComponent<Rigidbody>().velocity = tempVelocity;
    }
}
