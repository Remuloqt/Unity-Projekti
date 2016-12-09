using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarPlayerController : NetworkBehaviour
    {
        private GameObject cameraObject;
        private CarController m_Car; // the car controller we want to use

        // Use this for initialization
        void Start()
        {
            if (!isLocalPlayer) return;

            m_Car = GetComponent<CarController>();

            GameObject playerCarObject = this.gameObject;
            playerCarObject.AddComponent<CameraController>();

            // create camera object which contains the camera and the cameracontroller script
            // lastly attach it to the player object
            cameraObject = new GameObject("PlayerCamera");
            cameraObject.transform.parent = this.gameObject.transform;
            cameraObject.AddComponent<Camera>();
            cameraObject.AddComponent<CameraController>();

            CameraController cameraControllerScript = cameraObject.GetComponent<CameraController>();
            cameraControllerScript.camera = cameraObject.GetComponent<Camera>();
            cameraControllerScript.pivot = playerCarObject.transform;

        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }
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
