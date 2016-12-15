using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOutOfBoundsScript : MonoBehaviour {

    public int outOfBoundsCooldown = 10;
    public int outOfBoundsHealth = 100;
    private int outOfBoundsMaxHealth = 100;
    public int outOfBoundsDamage = 10;

    public bool enabled = true;

    private GameObject playerSpawnPointsMainObject;
    private List<Transform> playerSpawnPoints;
    private Transform[] playerSpawnPointArray;

	// Use this for initialization
	void Start () {

        outOfBoundsMaxHealth = outOfBoundsHealth;

        playerSpawnPointsMainObject = GameObject.Find("PlayerSpawnPoints");
        playerSpawnPoints = new List<Transform>();

        foreach (Transform child in playerSpawnPointsMainObject.transform)
        {
            playerSpawnPoints.Add(child);
        }

        playerSpawnPointArray = playerSpawnPoints.ToArray();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPlayerGroundHit()
    {
        if (!enabled) return;

        if (outOfBoundsCooldown > 0) outOfBoundsCooldown--;
        else
        {
            if (outOfBoundsHealth > 0) outOfBoundsHealth -= outOfBoundsDamage;
            else
            {
                outOfBoundsHealth = outOfBoundsMaxHealth;

                int randomPositionIndex = Random.Range(0, playerSpawnPointArray.Length);

                transform.position = playerSpawnPointArray[randomPositionIndex].position;
                transform.rotation = playerSpawnPointArray[randomPositionIndex].rotation;
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
    }
}
