using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostScript : MonoBehaviour {

    public float speedBoost = 10.0f;

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

        carObject.GetComponent<Rigidbody>().AddForce(transform.forward * -(speedBoost * 10), ForceMode.Acceleration);
    }
}
