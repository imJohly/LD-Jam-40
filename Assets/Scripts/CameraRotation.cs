using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

	[Header("Camera Settings")]
	public float minY;
	public float maxY;

	[Space]
	public float sensitivity;

	public float rotationSmoothTime;
	private Vector3 rotationSmoothVelocity;
	
	private float rotX, rotY;
	private Vector3 currentRotation;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;

		float rot = Input.GetAxis("Mouse X") * sensitivity;
		transform.parent.Rotate(0, rot, 0);

		rotX += Input.GetAxis("Mouse X") * sensitivity;
		rotY -= Input.GetAxis("Mouse Y") * sensitivity;

		rotY = Mathf.Clamp(rotY, -maxY, minY);
		
		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(rotY, rotX), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;
	}

	void FixedUpdate()
	{
		if(Input.GetMouseButtonDown(0))
			Cursor.lockState = CursorLockMode.Locked;

		// float rot = Input.GetAxis("Mouse X") * sensitivity;
		// transform.parent.Rotate(0, rot, 0);

		// rotX += Input.GetAxis("Mouse X") * sensitivity;
		// rotY -= Input.GetAxis("Mouse Y") * sensitivity;

		// rotY = Mathf.Clamp(rotY, -maxY, minY);
		
		// currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(rotY, rotX), ref rotationSmoothVelocity, rotationSmoothTime);
		// transform.eulerAngles = currentRotation;
	}
}
