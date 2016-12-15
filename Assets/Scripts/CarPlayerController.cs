using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarPlayerController : NetworkBehaviour
    {
        private GameObject cameraObject;
        private CarController m_Car; // the car controller we want to use

        public float cameraSpeed = 1f;
        public Transform cameraPosition;

        private GameObject playerSpawnPointsMainObject;
        private List<Transform> playerSpawnPoints;
        private Transform[] playerSpawnPointArray;

        // Use this for initialization
        void Start()
        {
            if (!isLocalPlayer) return;

            playerSpawnPointsMainObject = GameObject.Find("PlayerSpawnPoints");
            playerSpawnPoints = new List<Transform>();

            foreach (Transform child in playerSpawnPointsMainObject.transform)
            {
                playerSpawnPoints.Add(child);
            }
            playerSpawnPointArray = playerSpawnPoints.ToArray();

            GameObject playerCarObject = transform.gameObject;

            int randomArrayIndex = Random.Range(0, playerSpawnPointArray.Length);
            playerCarObject.transform.position = playerSpawnPointArray[randomArrayIndex].position;
            playerCarObject.transform.rotation = playerSpawnPointArray[randomArrayIndex].rotation;

            // create camera object which contains the camera and the cameracontroller script
            // lastly attach it to the player object
            cameraObject = GameObject.Find("PlayerCamera").gameObject;
            cameraObject.transform.position = cameraPosition.position;
            cameraObject.transform.rotation = transform.rotation;
            cameraObject.transform.parent = transform;
            cameraObject.AddComponent<Camera>();

            m_Car = GetComponent<CarController>();

        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer) return;

            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
