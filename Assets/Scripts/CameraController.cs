using System;
using UnityEngine;

public class CameraController : MonoBehaviour 
{	
	public float rotateSpeed = 1f, scrollSpeed = 200f;
    public Camera camera;
	public Transform pivot;
	public Vector3 cameraAssign = new Vector3(0,0,0);
	public SphericalCoordinates sc;

	private void Start()
	{
        if (camera == null)
        {
            sc = new SphericalCoordinates(this.gameObject.transform.Find("CameraPosition").transform.position, 3f, 10f, 0f, Mathf.PI * 2f, 0f, Mathf.PI / 2f);
        }
        else
        {
            sc = new SphericalCoordinates(camera.transform.position, 3f, 10f, 0f, Mathf.PI * 2f, 0f, Mathf.PI / 2f);
        }
		// Initialize position
        if (pivot == null) return;
        camera.transform.position = sc.toCartesian + pivot.position;
	}

	void Update () 
	{
		float h, v;

		h = Input.GetAxis( "Mouse X" );
		v = Input.GetAxis( "Mouse Y" );

        if (pivot == null) return;
        camera.transform.position = sc.Rotate(-h * rotateSpeed * Time.deltaTime, -v * rotateSpeed * Time.deltaTime).toCartesian + pivot.position;

        float sw = -Input.GetAxis("Mouse ScrollWheel");
        if (sw * sw > Mathf.Epsilon)
            camera.transform.position = sc.TranslateRadius(sw * Time.deltaTime * scrollSpeed).toCartesian + pivot.position;

        camera.transform.LookAt(pivot.position + cameraAssign);
	}
}