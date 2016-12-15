using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderOutOfBoundsScript : MonoBehaviour {

    private GameObject playerObject;

	// Use this for initialization
	void Start () {
        /*
        GameObject wheelsHub = transform.parent.gameObject;
        playerObject = wheelsHub.transform.parent.gameObject;
        */
        GameObject collidersObject = transform.parent.gameObject;
        playerObject = collidersObject.transform.parent.gameObject;
	}
	
    /*
	void FixedUpdate () {
        WheelHit hit;
        WheelCollider wheel = GetComponent<WheelCollider>();
        if (wheel.GetGroundHit(out hit))
        {
            if(hit.collider.gameObject.name == "Terrain")
            {
                CarOutOfBoundsScript carOutOfBoundsScript = playerObject.GetComponent<CarOutOfBoundsScript>();
                carOutOfBoundsScript.OnPlayerGroundHit();
            }
        }
	}
    */

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == "Terrain")
        {
            CarOutOfBoundsScript carOutOfBoundsScript = playerObject.GetComponent<CarOutOfBoundsScript>();
            carOutOfBoundsScript.OnPlayerGroundHit();
        }
    }
}
