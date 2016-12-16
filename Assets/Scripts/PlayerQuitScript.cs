using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerQuitScript : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    [Command]
    void CmdExitGame(GameObject playerObject)
    {
        RpcExitGame(playerObject);
    }

    [ClientRpc]
    void RpcExitGame(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy);

        if (isLocalPlayer)
        {
            GameObject startAndEndScreenManager = GameObject.Find("StartAndEndScreenManager");
            var startAndEndScreenManagerScript = startAndEndScreenManager.GetComponent<StartAndEndScreenManagerScript>();
            GameObject playerCarObject = transform.gameObject;
            CarPlayerData carPlayerData = playerCarObject.GetComponent<CarPlayerData>();

            GetComponent<LapManager>().RemoveLapsDrivenNumberText();
            startAndEndScreenManagerScript.EndGame(carPlayerData);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape) && isLocalPlayer)
        {
            CmdExitGame(gameObject);
        }
	}
}
