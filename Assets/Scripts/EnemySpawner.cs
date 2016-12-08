using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{

    public GameObject enemyPrefab;
    public int numberOfEnemies;

    private NetworkStartPosition[] spawnPoints;

    public override void OnStartServer()
    {
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        List<NetworkStartPosition> enemySpawnPointList = new List<NetworkStartPosition>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if(spawnPoints[i].tag == "EnemySpawn")
            {
                enemySpawnPointList.Add(spawnPoints[i]);
            }
        }

        NetworkStartPosition[] enemySpawnPoints = enemySpawnPointList.ToArray();

        for (int i = 0; i < numberOfEnemies; i++)
        {
            var spawnPosition = enemySpawnPoints[(i % enemySpawnPoints.Length)].transform.position;

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }
}