using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[Header("Movement Settings")]
	public float moveSpeed = 10;

	public float gravityScale = 4;

	private float x, z;
	private Vector3 movement;

	private CharacterController controller;

	void Start() 
	{
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		// var fps = 1.0 / Time.deltaTime;
		// print("FPS: " + fps);

		if(transform.position.y < -200)
			transform.position = new Vector3(transform.position.x, 200, transform.position.z);
	}

	void FixedUpdate()
	{
		z = Input.GetAxis("Vertical") * moveSpeed;
		x = Input.GetAxis("Horizontal") * moveSpeed * 0.75f;

		movement = new Vector3(x, movement.y, z);
		
		Vector2 tempIn = new Vector2(x, z);
		Vector2 inDir = tempIn.normalized;

		movement = transform.rotation * movement;

		if(!controller.isGrounded)
			movement.y = movement.y + Physics.gravity.y * gravityScale * Time.deltaTime;
		
		if(controller.isGrounded)
			movement.y = 0;

		controller.Move(movement * Time.deltaTime);

		if(new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude > 0.1f && controller.isGrounded)
			StartCoroutine(Walking());

		if(controller.velocity.magnitude <= 0)
			AudioManager.instance.Stop("Walking");

	}

	bool running;
	IEnumerator Walking()
	{
		if(running == false)
		{
			running = true;
			AudioManager.instance.Play("Walking");
			yield return new WaitForSeconds(0.5f);
			running = false;
		}
	}
}
