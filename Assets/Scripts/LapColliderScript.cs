using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapColliderScript : MonoBehaviour {

    private LapManager lapManager;

	// Use this for initialization
	void Start () {
        GameObject collidersObject = transform.parent.gameObject;
        GameObject playerObject = collidersObject.transform.parent.gameObject;
        lapManager = playerObject.GetComponent<LapManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LapEnd")
        {
            lapManager.LapEndPassed();
        }

        if (other.gameObject.name == "LapStart")
        {
            lapManager.LapStartPassed();
        }
    }
}
