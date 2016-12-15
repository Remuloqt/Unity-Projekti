using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOutOfBoundsScript : MonoBehaviour {

    private GameObject playerObject;

	// Use this for initialization
	void Start () {
        GameObject wheelsHub = transform.parent.gameObject;
        playerObject = wheelsHub.transform.parent.gameObject;
	}
	
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

}
