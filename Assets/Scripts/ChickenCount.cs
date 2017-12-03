using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCount : MonoBehaviour {

	public Vector3 size;
	public LayerMask chickenLayer;
	public Collider[] chickens;
	public int outChicken;
	[Space]

	public GameObject winScreen;
	public GameObject normScreen;

	void Start()
	{
		winScreen.SetActive(false);
		normScreen.SetActive(true);
	}

	int lastCount;

	void Update()
	{
		chickens = Physics.OverlapBox(transform.position, size, transform.rotation, chickenLayer);
		Player.inst.chickenAmount = chickens.Length;

		if(Player.inst.chickenAmount >= 21)
		{
			Win();
		}

		if(chickens.Length != lastCount)
		{
			AudioManager.instance.Play("Add Chook");
			lastCount = chickens.Length;
		}
	}

	void OnDrawGizmos()
	{
		Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
		Gizmos.matrix = rotationMatrix;

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(Vector3.zero, size * 2);
	}

	bool done;
	void Win()
	{
		Cursor.lockState = CursorLockMode.None;

		winScreen.SetActive(true);
		normScreen.SetActive(false);

		if(done == false)		
		{
			done = true;

			Player.inst.GetComponent<PlayerMovement>().enabled = false;
			FindObjectOfType<CameraRotation>().enabled = false;
			FindObjectOfType<PickupChicken>().enabled = false;

			AudioManager.instance.Play("Win Theme");
		}
	}
}
