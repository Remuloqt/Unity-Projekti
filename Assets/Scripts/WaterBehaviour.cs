using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour {

	GameObject playerObject;
	bool isInWater = false;
	private Transform[] playerSpawnPointArray;
	private List<Transform> playerSpawnPoints;
	public int timer = 150;

	// Use this for initialization
	void Start () {
		GameObject playerSpawnPointsMainObject = GameObject.Find("PlayerSpawnPoints");
		playerSpawnPoints = new List<Transform>();

		foreach (Transform child in playerSpawnPointsMainObject.transform)
		{
			playerSpawnPoints.Add(child);
		}

		playerSpawnPointArray = playerSpawnPoints.ToArray();
	}
	
	// Update is called once per frame
	void Update () {
		if(isInWater){
			playerObject.GetComponent<Rigidbody> ().velocity /= 1.1f;
			timer -= 1;
			if (timer < 0) {
				int randomArrayIndex = Random.Range(0, playerSpawnPointArray.Length);
				playerObject.transform.position = playerSpawnPointArray[randomArrayIndex].position;
				playerObject.transform.rotation = playerSpawnPointArray[randomArrayIndex].rotation;
				}
			}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag != "Player") {
			return;
		}
		GameObject collidersObject = collider.transform.parent.gameObject;
		GameObject carObject = collidersObject.transform.parent.gameObject;
		playerObject = carObject;
		if (playerObject.tag == "Player") {
			isInWater = true;
		}

	}
	void OnTriggerExit(Collider collider){
		isInWater = false;
	}

	
}
