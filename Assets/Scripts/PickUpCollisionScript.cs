using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class PickUpCollisionScript : NetworkBehaviour {

    public int scoreAddition = 10;

    void OnTriggerEnter(Collider other)
    {
        GameObject carPlayerObject = null;
        try
        {
            GameObject collidersObject = other.gameObject.transform.parent.gameObject;
            carPlayerObject = collidersObject.transform.parent.gameObject;
        }
        catch
        {
            return;
        }
        if (carPlayerObject != null && carPlayerObject.tag == "Player")
        {
            Debug.Log("Pickup collected!");

            CarPlayerData playerScoreScript = carPlayerObject.GetComponent<CarPlayerData>();

            playerScoreScript.OnPickupPickedUp(scoreAddition);

            NetworkServer.Destroy(gameObject);
        }
    }
}
