﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{

    public const int maxHealth = 100;
    public bool destroyOnDeath;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;

    private NetworkStartPosition[] spawnPoints;

    void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    [ClientRpc]
    void RpcDestroyOnDeath(GameObject objectToDestroy)
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

    public void AddHealth(int amount)
    {
        if (!isServer)
        {
            return;
        }

        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                RpcDestroyOnDeath(gameObject);
            }
            else
            {
                currentHealth = maxHealth;

                // called on the Server, invoked on the Clients
                RpcRespawn();
            }
        }
    }

    void OnChangeHealth(int currentHealth)
    {
        if (healthBar == null) return;
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            // If there is a spawn point array and the array is not empty, pick one at random
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            // Set the player’s position to the chosen spawn point
            transform.position = spawnPoint;
        }
    }
}