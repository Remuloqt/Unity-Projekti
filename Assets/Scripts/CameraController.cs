using System;
using UnityEngine;

public class CameraController : MonoBehaviour 
{	
	public float rotateSpeed = 1f, scrollSpeed = 200f;
	public Transform pivot;
	public Vector3 cameraAssign = new Vector3(0,0,0);
	private SphericalCoordinates sc;
    
	void Start()
	{
        sc = new SphericalCoordinates(transform.Find("CarPlayer").transform.position, 3f, 10f, 0f, Mathf.PI * 2f, 0f, Mathf.PI / 2f);
        Debug.Log("Spherical coordinates created: " + sc);
        // Initialize position
        if (pivot == null)
        {
            Debug.Log("Pivot is null");
            return;
        }
        Debug.Log("Using Pivot:", pivot);
        Debug.Log("Sc.toCartesian = " + sc.toCartesian);
        transform.position = sc.toCartesian + pivot.position;
	}    

	void Update () 
	{
		float h, v;

		h = Input.GetAxis( "Mouse X" );
		v = Input.GetAxis( "Mouse Y" );

        if (pivot == null) return;
        //Debug.Log("Pivot position: " + pivot.position);
        //Debug.Log("Spherical coordinates: " + sc);
        transform.position = sc.Rotate(-h * rotateSpeed * Time.deltaTime, -v * rotateSpeed * Time.deltaTime).toCartesian + pivot.position;

        float sw = -Input.GetAxis("Mouse ScrollWheel");
        if (sw * sw > Mathf.Epsilon)
            transform.position = sc.TranslateRadius(sw * Time.deltaTime * scrollSpeed).toCartesian + pivot.position;

        transform.LookAt(pivot.position + cameraAssign);
	}
}