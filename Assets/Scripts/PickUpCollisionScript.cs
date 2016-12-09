using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class PickUpCollisionScript : NetworkBehaviour {

    public int healthAddition = 10;


    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Pickup collected!");

            var colliderObject = other.gameObject;
            var collidersObject = colliderObject.transform.parent.gameObject;
            var carPlayerObject = collidersObject.transform.parent.gameObject;
            var healthScript = carPlayerObject.GetComponent<Health>();
            healthScript.AddHealth(healthAddition);

            ScoreManager.score++;

            NetworkServer.Destroy(this.gameObject);

        }
    }
}
