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

	void Update()
	{
		chickens = Physics.OverlapBox(transform.position, size, transform.rotation, chickenLayer);
		Player.inst.chickenAmount = chickens.Length;

		if(Player.inst.chickenAmount >= 21)
		{
			Win();
		}
	}

	void OnDrawGizmos()
	{
		Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
		Gizmos.matrix = rotationMatrix;

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(Vector3.zero, size * 2);
	}

	void Win()
	{
		Cursor.lockState = CursorLockMode.None;

		winScreen.SetActive(true);
		normScreen.SetActive(false);

		Time.timeScale = 0;
	}
}
