using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PickupSpawner : NetworkBehaviour {

    public GameObject pickupPrefab;
    public int numberOfPickups;

    private NetworkStartPosition[] spawnPoints;

    public override void OnStartServer()
    {
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        List<NetworkStartPosition> pickupSpawnPointList = new List<NetworkStartPosition>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].tag == "PickupSpawn")
            {
                pickupSpawnPointList.Add(spawnPoints[i]);
            }
        }

        NetworkStartPosition[] pickupSpawnPoints = pickupSpawnPointList.ToArray();

        for (int i = 0; i < numberOfPickups; i++)
        {
            var spawnPosition = pickupSpawnPoints[(i % pickupSpawnPoints.Length)].transform.position;

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);

            var pickup = (GameObject)Instantiate(pickupPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(pickup);
        }
    }
}
