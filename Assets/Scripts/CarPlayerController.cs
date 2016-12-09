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

        public Camera camera;

        private CarController m_Car; // the car controller we want to use

        // Use this for initialization
        void Start()
        {
            m_Car = GetComponent<CarController>();
            Camera mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
            CameraController cameraControllerScript = mainCamera.GetComponent<CameraController>();
            mainCamera.transform.position = this.transform.Find("CameraPosition").transform.position;
            cameraControllerScript.pivot = this.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer)
            {
                camera.enabled = false;
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
