using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class PickUpCollisionScript : NetworkBehaviour {

    public int scoreAddition = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Pickup collected!");

            var colliderObject = other.gameObject;
            var collidersObject = colliderObject.transform.parent.gameObject;
            var carPlayerObject = collidersObject.transform.parent.gameObject;

            CarPlayerData playerScoreScript = carPlayerObject.GetComponent<CarPlayerData>();

            playerScoreScript.OnPickupPickedUp(scoreAddition);

            NetworkServer.Destroy(gameObject);
        }
    }
}
