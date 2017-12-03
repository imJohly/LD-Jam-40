using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupChicken : MonoBehaviour {

	public Transform hand;
	private bool isHolding;
	[Space]
	public float distanceGrip = 2;
	public LayerMask chickenLayer;
	[Space]
	public GameObject pitchFork;

	void Start()
	{
		pitchFork.GetComponent<Collider>().enabled = false;
	}

	RaycastHit hit;
	void Update()
	{	
		if(Input.GetMouseButtonDown(1) && isHolding == false)
		{
			if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanceGrip, chickenLayer))
			{
				if(hit.transform.tag == "Chicken")
				{
					hit.transform.gameObject.GetComponent<AIChicken>().enabled = false;
					hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;

					isHolding = true;

					pitchFork.SetActive(false);
				}
			}
		}
		else if(Input.GetMouseButtonDown(1) && isHolding)
		{
			hit.transform.gameObject.GetComponent<AIChicken>().enabled = true;
			hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;

			isHolding = false;

			pitchFork.SetActive(true);
		}

		if(Input.GetMouseButtonDown(0) && isHolding == false)
		{
			StartCoroutine(Attack());
		}
	}

	void LateUpdate()
	{
		if(isHolding)
		{
			hit.transform.position = hand.position;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward);
	}

	IEnumerator Attack()
	{
		pitchFork.GetComponent<Animator>().SetTrigger("Attack");
		pitchFork.GetComponent<Collider>().enabled = true;
		yield return new WaitForSeconds(1);
		pitchFork.GetComponent<Collider>().enabled = false;
	}
}
