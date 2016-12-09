using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {
    
    public float rotateSpeed = 1f, scrollSpeed = 200f;
    private Transform pivot;
    public Camera camera;
    public Vector3 cameraAssign = new Vector3(0, 0, 0);
    public SphericalCoordinates sc;



	// Use this for initialization
	void Start () {
        pivot = this.transform;

        sc = new SphericalCoordinates(camera.transform.position, 3f, 10f, 0f, Mathf.PI * 2f, 0f, Mathf.PI / 2f);
        // Initialize position
        if (camera == null) return;
        camera.transform.position = sc.toCartesian + pivot.position;
	}
	
	// Update is called once per frame
	void Update () {

        float h, v;

        h = Input.GetAxis("Mouse X");
        v = Input.GetAxis("Mouse Y");

        if (camera == null) return;
        camera.transform.position = sc.Rotate(-h * rotateSpeed * Time.deltaTime, -v * rotateSpeed * Time.deltaTime).toCartesian + pivot.position;

        float sw = -Input.GetAxis("Mouse ScrollWheel");
        if (sw * sw > Mathf.Epsilon)
            camera.transform.position = sc.TranslateRadius(sw * Time.deltaTime * scrollSpeed).toCartesian + pivot.position;

        camera.transform.LookAt(pivot.position + cameraAssign);
	}
}
