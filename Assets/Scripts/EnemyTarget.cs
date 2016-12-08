﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyTarget : NetworkBehaviour {

    private NavMeshAgent agent;
    private Transform myTransform;
    private Transform targetTransform;
    private LayerMask raycastLayer;
    private float radius = 100;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float cooldown = 100.0f;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        myTransform = transform;
        raycastLayer = 1 << LayerMask.NameToLayer("Player");
	}
	
	void FixedUpdate()
    {
        SearchForTarget();
        MoveToTarget();

        if (cooldown > 0)
        {
            cooldown -= 1.0f;
            return;
        }
        ShootAtTarget();
    }

    void SearchForTarget()
    {
        if (!isServer) return;

        if (targetTransform == null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(myTransform.position, radius, raycastLayer);

            if (hitColliders.Length > 0)
            {
                int randomint = Random.Range(0, hitColliders.Length);
                targetTransform = hitColliders[randomint].transform;
            }
        }

        if (targetTransform != null && targetTransform.GetComponent<Collider>().enabled == false)
        {
            targetTransform = null;
        }
    }

    void MoveToTarget()
    {
        if (targetTransform != null && isServer)
        {
            SetNavDestination(targetTransform);
        }
    }

    void ShootAtTarget()
    {
        if (targetTransform != null && isServer && bulletPrefab != null && bulletSpawn != null)
        {
            var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 35;
            NetworkServer.Spawn(bullet);
            Destroy(bullet, 2.0f);
        }
        cooldown = 100.0f;
    }

    void SetNavDestination(Transform dest)
    {
        agent.SetDestination(dest.position);
    }
}
